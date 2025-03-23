using Fiap2025.Entrega3.Application.Handlers;
using Fiap2025.Entrega3.Domain.Interfaces;
using Fiap2025.Entrega3.Infrastructure;
using Fiap2025.Entrega3.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


var configuration = builder.Configuration;
var rabbitMQHost = configuration["RabbitMQ:HostName"] ?? throw new ArgumentNullException("RabbitMQ Host");
var rabbitMQUsername = configuration["RabbitMQ:UserName"] ?? throw new ArgumentNullException("RabbitMQ Username");
var rabbitMQPassword = configuration["RabbitMQ:Password"] ?? throw new ArgumentNullException("RabbitMQ Password");


builder.Services.AddSingleton<RabbitMQConnection>(provider =>
{
    var hostname = rabbitMQHost;
    var username = rabbitMQUsername;
    var password = rabbitMQPassword;
    return new RabbitMQConnection(hostname, username, password);
});


var connectionString = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();


var serviceProvider = builder.Services.BuildServiceProvider();
var rabbitMQConnection = serviceProvider.GetRequiredService<RabbitMQConnection>();

builder.Services.AddSingleton<IContatoRepository>(provider => new ContatoRepository(rabbitMQConnection, connectionString));


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddContatoHandler).Assembly)); ;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
