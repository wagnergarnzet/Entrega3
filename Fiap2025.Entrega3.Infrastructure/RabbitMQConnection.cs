using RabbitMQ.Client;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Fiap2025.Entrega3.Infrastructure
{
    public class RabbitMQConnection
    {
        private readonly Task<IConnection> _connectionTask;

        public RabbitMQConnection(string hostname, string username, string password)
        {
            var factory = new ConnectionFactory() { HostName = hostname, UserName = username, Password = password, Port = 5672 };
            _connectionTask = factory.CreateConnectionAsync();
        }

        public async Task<IChannel> CreateChannelAsync()
        {
            var connection = await _connectionTask.ConfigureAwait(false);
            IChannel channel = await connection.CreateChannelAsync();
            return channel;
        }
    }
}


