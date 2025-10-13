using GerenciadorInventario.EstoqueAPI.Context;
using GerenciadorInventario.EstoqueAPI.Models;

namespace GerenciadorInventario.EstoqueAPI.Seed;

public static class EstoqueDbContextSeed
{
    public static async Task SeedAsync(this EstoqueDbContext ctx)
    {
        if (ctx.Estoques.Any()) return;

        ctx.Estoques.AddRange(
            new Estoque { ProdutoId = 1, Quantidade = 50 },
            new Estoque { ProdutoId = 2, Quantidade = 30 }
        );
        await ctx.SaveChangesAsync();
    }
}
