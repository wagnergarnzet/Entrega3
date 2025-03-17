using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiap2025.Entrega3.Domain.Entities;
using Fiap2025.Entrega3.Domain.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Fiap2025.Entrega3.Infrastructure.Repositories
{
    public class ContatoRepository : IContatoRepository
    {

        private readonly RabbitMQConnection _rabbitMQConnection;

        public ContatoRepository(RabbitMQConnection rabbitMQConnection)
        {
            _rabbitMQConnection = rabbitMQConnection;
        }

        private async Task postar_mensagem(string fila, Contato contact)
        {
            using (var channel = await _rabbitMQConnection.CreateChannelAsync())
            {
                await channel.QueueDeclareAsync(queue: fila,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var message = JsonConvert.SerializeObject(contact);
                var body = Encoding.UTF8.GetBytes(message);

                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: fila, body: body);

            }
        }

        public async Task AddContatoAsync(Contato contact)
        {
            var fila = "add_contato";
            await postar_mensagem(fila, contact);
        }


        public async Task DeleteContatoAsync(Contato contact)
        {
            var fila = "delete_contato";
            await postar_mensagem(fila, contact);
        }

        public Task<IEnumerable<Contato>> GetAllContatosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Contato> GetContatoByDDDAsync(string DDD)
        {
            throw new NotImplementedException();
        }

        public Task<Contato> GetContatoByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateContatoAsync(Contato contact)
        {
            var fila = "update_contato";
            await postar_mensagem(fila, contact);
        }
    }
}
