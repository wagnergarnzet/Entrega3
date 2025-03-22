using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Fiap2025.Entrega3.Domain.Entities;
using Fiap2025.Entrega3.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Fiap2025.Entrega3.Infrastructure.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly RabbitMQConnection _rabbitMQConnection;
        private readonly string _connectionString;

        public ContatoRepository(RabbitMQConnection rabbitMQConnection, IConfiguration configuration)
        {
            _rabbitMQConnection = rabbitMQConnection;
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
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

        public async Task<IEnumerable<Contato>> GetAllContatosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "select Id,Name,AreaCode,Phone,Email  from [dbo].[Contatos]";
                return await connection.QueryAsync<Contato>(query);
            }
        }

        public async Task<IEnumerable<Contato>> GetContatoByDDDAsync(string DDD)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Contato>(
                    "SELECT  Id,Name,AreaCode,Phone,Email FROM Contatos WHERE DDD = @DDD", new { DDD });
            }
        }

        public async Task<Contato?> GetContatoByIdAsync(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT Id,Name,AreaCode,Phone,Email FROM Contatos WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Contato>(query, new { Id = id });
            }
        }

        public async Task UpdateContatoAsync(Contato contact)
        {
            var fila = "update_contato";
            await postar_mensagem(fila, contact);
        }
    }
}
