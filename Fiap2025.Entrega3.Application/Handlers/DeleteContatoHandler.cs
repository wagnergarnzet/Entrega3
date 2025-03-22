using Fiap2025.Entrega3.Application.Commands;
using Fiap2025.Entrega3.Domain.Entities;
using Fiap2025.Entrega3.Domain.Interfaces;
using MediatR;

namespace Fiap2025.Entrega3.Application.Handlers
{
    public class DeleteContatoHandler : IRequestHandler<DeleteContatoCommand>

    {
        private readonly IContatoRepository _repository;

        public DeleteContatoHandler(IContatoRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(DeleteContatoCommand request, CancellationToken cancellationToken)
        {
            var contato = await _repository.GetContatoByIdAsync(request.Id);

            if (contato != null)
            {
                await _repository.DeleteContatoAsync(contato);
            }
        }
    }
}
