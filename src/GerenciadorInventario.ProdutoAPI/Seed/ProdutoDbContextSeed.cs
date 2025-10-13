using GerenciadorInventario.ProdutoAPI.Context;
using GerenciadorInventario.ProdutoAPI.Models;

namespace GerenciadorInventario.ProdutoAPI.Seed;

public static class ProdutoDbContextSeed
{
    public static async Task SeedAsync(this ProdutoDbContext ctx)
    {
        if (ctx.Produtos.Any()) return;

        var p1 = new Produto("Teclado", 120m, "Teclado mecânico");
        p1.Validar();
        var p2 = new Produto("Mouse", 80m, "Mouse óptico");
        p2.Validar();

        ctx.Produtos.AddRange(p1, p2);
        await ctx.SaveChangesAsync();
    }
}
