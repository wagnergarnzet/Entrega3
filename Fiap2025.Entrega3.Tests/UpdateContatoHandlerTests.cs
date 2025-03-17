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
    public class UpdateContatoHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldUpdateContato()
        {
            // Arrange
            var repositoryMock = new Mock<IContatoRepository>();
            var handler = new UpdateContatoHandler(repositoryMock.Object);
            var command = new UpdateContatoCommand
            {
                Id = Guid.NewGuid(),
                Name = "Wagner Garnizet",
                Email = "wagnergarnzet@hotmail.com",
                Phone = "967918795",
                AreaCode = "11"
            };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            repositoryMock.Verify(r => r.UpdateContatoAsync(It.IsAny<Contato>()), Times.Once);
        }
    }
}