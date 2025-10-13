using Microsoft.EntityFrameworkCore;

namespace GerenciadorInventario.ProdutoAPI.Context;

public class ProdutoDbContext : DbContext
{
    public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options) : base(options) { }

    public DbSet<Models.Produto> Produtos => Set<Models.Produto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Produto>(e =>
        {
            e.ToTable("Produtos");
            e.HasKey(p => p.Id);
            e.Property(p => p.Nome).IsRequired();
            e.HasIndex(p => p.Nome).IsUnique();
            e.Property(p => p.Preco).HasPrecision(18, 2);
        });
    }
}
