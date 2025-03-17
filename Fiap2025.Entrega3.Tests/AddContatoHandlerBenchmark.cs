using BenchmarkDotNet.Attributes;
using Fiap2025.Entrega3.Application.Commands;
using Fiap2025.Entrega3.Application.Handlers;
using Fiap2025.Entrega3.Domain.Interfaces;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap2025.Entrega3.Tests
{
    public class AddContatoHandlerBenchmark
    {
        private AddContatoHandler _handler;
        private AddContatoCommand _command;

        [GlobalSetup]
        public void Setup()
        {
            var repositoryMock = new Mock<IContatoRepository>();
            _handler = new AddContatoHandler(repositoryMock.Object);
            _command = new AddContatoCommand
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "123456789",
                AreaCode = "11"
            };
        }

        [Benchmark]
        public async Task Handle_AddContato()
        {
            await _handler.Handle(_command, CancellationToken.None);
        }
    }
}