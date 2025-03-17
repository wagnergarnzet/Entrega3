using Fiap2025.Entrega3.Application.Commands;
using Fiap2025.Entrega3.Application.Handlers;
using Fiap2025.Entrega3.Domain.Entities;
using Fiap2025.Entrega3.Domain.Interfaces;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xunit;

namespace Fiap2025.Entrega3.Tests
{
    public class AddContatoHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldAddContato()
        {
            // Arrange
            var repositoryMock = new Mock<IContatoRepository>();
            var handler = new AddContatoHandler(repositoryMock.Object);
            var command = new AddContatoCommand
            {
                Name = "Wagner Garnizet da Silva",
                Email = "wagner.garnizet@example.com",
                Phone = "967918795",
                AreaCode = "11"
            };

            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            repositoryMock.Verify(r => r.AddContatoAsync(It.IsAny<Contato>()), Times.Once);
            Assert.NotEqual(Guid.Empty, result);
        }
    }
}