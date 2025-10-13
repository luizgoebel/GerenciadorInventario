using Microsoft.EntityFrameworkCore;

namespace GerenciadorInventario.ReciboAPI.Context;

public class ReciboDbContext : DbContext
{
    public ReciboDbContext(DbContextOptions<ReciboDbContext> options) : base(options) { }

    public DbSet<Models.Recibo> Recibos => Set<Models.Recibo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Recibo>(e =>
        {
            e.ToTable("Recibos");
            e.HasKey(r => r.Id);
            e.Property(r => r.Numero)
                .IsRequired()
                .HasMaxLength(50);
            e.HasIndex(r => r.Numero).IsUnique();

            e.Property(r => r.FaturaId).IsRequired();
            e.Property(r => r.DataEmissao)
                .IsRequired()
                .HasColumnType("datetime2");
            e.Property(r => r.ValorTotal)
                .HasPrecision(18, 2);
        });
    }
}
