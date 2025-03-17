using Fiap2025.Entrega3.Application.Commands;
using Fiap2025.Entrega3.Application.Handlers;
using Fiap2025.Entrega3.Domain.Entities;
using Fiap2025.Entrega3.Domain.Interfaces;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Fiap2025.Entrega3.Tests
{
    public class DeleteContatoHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldDeleteContato()
        {
            // Arrange
            var repositoryMock = new Mock<IContatoRepository>();
            var handler = new DeleteContatoHandler(repositoryMock.Object);
            var command = new DeleteContatoCommand
            {
                Id = Guid.NewGuid()
            };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            repositoryMock.Verify(r => r.DeleteContatoAsync(It.IsAny<Contato>()), Times.Once);
        }
    }
}