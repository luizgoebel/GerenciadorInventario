using GerenciadorInventario.PedidoAPI.Context;
using GerenciadorInventario.PedidoAPI.Models;

namespace GerenciadorInventario.PedidoAPI.Seed;

public static class PedidoDbContextSeed
{
    public static async Task SeedAsync(this PedidoDbContext ctx)
    {
        if (ctx.Pedidos.Any()) return;

        var p1 = new Pedido();
        p1.AdicionarItem(1, 2, 120m);
        p1.Validar();
        var p2 = new Pedido();
        p2.AdicionarItem(2, 1, 80m);
        p2.Validar();

        ctx.Pedidos.AddRange(p1, p2);
        await ctx.SaveChangesAsync();
    }
}
