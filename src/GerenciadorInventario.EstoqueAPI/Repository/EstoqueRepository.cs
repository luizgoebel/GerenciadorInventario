using GerenciadorInventario.EstoqueAPI.Context;
using GerenciadorInventario.EstoqueAPI.Models;
using GerenciadorInventario.EstoqueAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorInventario.EstoqueAPI.Repository;

public class EstoqueRepository : Repository<Estoque>, IEstoqueRepository
{
    public EstoqueRepository(EstoqueDbContext ctx) : base(ctx) { }

    public async Task<Estoque?> GetByProdutoIdAsync(int produtoId)
    {
        return await _ctx.Estoques.FirstOrDefaultAsync(e => e.ProdutoId == produtoId);
    }
}
