using GerenciadorInventario.EstoqueAPI.Models;

namespace GerenciadorInventario.EstoqueAPI.Repository.Interface;

public interface IEstoqueRepository : IRepository<Estoque>
{
    Task<Estoque?> GetByProdutoIdAsync(int produtoId);
}
