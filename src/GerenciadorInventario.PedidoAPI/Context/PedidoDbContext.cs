using GerenciadorInventario.PedidoAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace GerenciadorInventario.PedidoAPI.Context;

public class PedidoDbContext : DbContext
{
    public PedidoDbContext(DbContextOptions<PedidoDbContext> options) : base(options) { }

    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<PedidoItem> PedidoItens => Set<PedidoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pedido>(e =>
        {
            e.ToTable("Pedidos");
            e.HasKey(p => p.Id);
            e.Property(p => p.Total).HasPrecision(18, 2);
            e.HasMany(p => p.Itens)
             .WithOne()
             .HasForeignKey(i => i.PedidoId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PedidoItem>(e =>
        {
            e.ToTable("PedidoItens");
            e.HasKey(i => i.Id);
            e.Property(i => i.PrecoUnitario).HasPrecision(18, 2);
        });
    }
}
