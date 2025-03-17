using Fiap2025.Entrega3.Domain.Entities;


namespace Fiap2025.Entrega3.Domain.Interfaces
{
    public interface IContatoRepository
    {
        Task AddContatoAsync(Contato contact);
        Task<Contato> GetContatoByIdAsync(Guid id);
        Task<Contato> GetContatoByDDDAsync(string DDD);
        Task<IEnumerable<Contato>> GetAllContatosAsync();
        Task UpdateContatoAsync(Contato contact);
        Task DeleteContatoAsync(Contato contact);

    }
}
