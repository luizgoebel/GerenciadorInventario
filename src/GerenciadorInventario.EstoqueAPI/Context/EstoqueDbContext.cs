using GerenciadorInventario.EstoqueAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorInventario.EstoqueAPI.Context;

public class EstoqueDbContext : DbContext
{
    public EstoqueDbContext() { }
    public EstoqueDbContext(DbContextOptions<EstoqueDbContext> options) : base(options) { }

    public DbSet<Estoque> Estoques => Set<Estoque>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estoque>(e =>
        {
            e.ToTable("Estoques");
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.ProdutoId).IsUnique();
            e.Property(x => x.Quantidade).IsRequired();
        });
    }
}
