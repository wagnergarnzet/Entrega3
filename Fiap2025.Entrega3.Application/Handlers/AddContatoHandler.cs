using Fiap2025.Entrega3.Application.Commands;
using Fiap2025.Entrega3.Domain.Entities;
using Fiap2025.Entrega3.Domain.Interfaces;
using MediatR;

namespace Fiap2025.Entrega3.Application.Handlers
{
    public class AddContatoHandler : IRequestHandler<AddContatoCommand,Guid>

    {
        private readonly IContatoRepository _repository;

        public  AddContatoHandler (IContatoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(AddContatoCommand request, CancellationToken cancellationToken)
        {
            var contato = new Contato { 
                   Id = Guid.NewGuid(),
                   Name = request.Name,
                   Email = request.Email,
                   AreaCode = request.AreaCode,
                   Phone = request.Phone
            };

            await _repository.AddContatoAsync(contato);
            return contato.Id;
        }
    }
}
