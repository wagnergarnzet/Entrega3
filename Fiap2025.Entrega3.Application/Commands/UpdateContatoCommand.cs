using MediatR;


namespace Fiap2025.Entrega3.Application.Commands
{
    public class UpdateContatoCommand: IRequest
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string AreaCode { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
    }
}
