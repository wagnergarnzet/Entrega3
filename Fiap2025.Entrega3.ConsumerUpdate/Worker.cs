using Fiap2025.Entrega3.Domain.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Fiap2025.Entrega3.ConsumerUpdate
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _configuration["RabbitMQ:HostName"] ?? throw new ArgumentNullException("RabbitMQ:HostName"),
                    Port = 5672,
                    UserName = _configuration["RabbitMQ:UserName"] ?? throw new ArgumentNullException("RabbitMQ:UserName"),
                    Password = _configuration["RabbitMQ:Password"] ?? throw new ArgumentNullException("RabbitMQ:Password")
                };

                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();
                {
                    await channel.QueueDeclareAsync(
                                         queue: "update_contato",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new AsyncEventingBasicConsumer(channel);

                    try
                    {
                        consumer.ReceivedAsync += async (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                            Contato contato = JsonSerializer.Deserialize<Contato>(message) ?? throw new ArgumentNullException("Contato inválido");

                            using (var scope = _serviceProvider.CreateScope())
                            {
                                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                                dbContext.Contatos.Update(contato);
                                await dbContext.SaveChangesAsync();
                            }

                            if (_logger.IsEnabled(LogLevel.Information))
                            {
                                _logger.LogInformation("Contato atualizado com sucesso {0}", message);
                            }

                        };

                        await channel.BasicConsumeAsync(
                                                         queue: "update_contato",
                                                         autoAck: true,
                                                         consumer: consumer);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Erro ao atualizar o contato");
                    }
                };
                await Task.CompletedTask;
            };


            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
