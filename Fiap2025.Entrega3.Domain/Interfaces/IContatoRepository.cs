using Fiap2025.Entrega3.Domain.Entities;


namespace Fiap2025.Entrega3.Domain.Interfaces
{
    public interface IContatoRepository
    {
        Task AddContatoAsync(Contato contato);
        Task<Contato?> GetContatoByIdAsync(Guid id);
        Task<IEnumerable<Contato>> GetContatoByDDDAsync(string DDD);
        Task<IEnumerable<Contato>> GetAllContatosAsync();
        Task UpdateContatoAsync(Contato contato);
        Task DeleteContatoAsync(Contato contato);

    }
}
