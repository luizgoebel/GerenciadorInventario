using GerenciadorInventario.ProdutoAPI.Context;
using GerenciadorInventario.ProdutoAPI.Repository.Interface;

namespace GerenciadorInventario.ProdutoAPI.Repository;

public class ProdutoRepository : Repository<Models.Produto>, IProdutoRepository
{
    public ProdutoRepository(ProdutoDbContext ctx) : base(ctx) { }
}
