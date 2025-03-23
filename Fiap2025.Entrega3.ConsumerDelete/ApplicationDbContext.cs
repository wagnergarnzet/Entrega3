using Fiap2025.Entrega3.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Fiap2025.Entrega3.ConsumerDelete
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Contato> Contatos { get; set; }
    }
}
