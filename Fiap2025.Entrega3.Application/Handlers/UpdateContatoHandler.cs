using Fiap2025.Entrega3.Application.Commands;
using Fiap2025.Entrega3.Domain.Entities;
using Fiap2025.Entrega3.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap2025.Entrega3.Application.Handlers
{
    public class UpdateContatoHandler : IRequestHandler<UpdateContatoCommand>
    {
        private readonly IContatoRepository _repository;

        public UpdateContatoHandler(IContatoRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(UpdateContatoCommand request, CancellationToken cancellationToken)
        {
            var contato = new Contato
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
                AreaCode = request.AreaCode,
                Phone = request.Phone
            };

            _repository.UpdateContatoAsync(contato);

            return Task.CompletedTask;
        }
    }
}