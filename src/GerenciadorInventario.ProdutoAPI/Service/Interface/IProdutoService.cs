using GerenciadorInventario.ProdutoAPI.Dto;

namespace GerenciadorInventario.ProdutoAPI.Service.Interface;

public interface IProdutoService
{
    Task<ProdutoDto> CriarProdutoAsync(ProdutoCriacaoDto dto);
    Task<ProdutoDto?> GetProdutoByIdAsync(int id);
    Task<IEnumerable<ProdutoDto>> GetTodosAsync();
    Task<ProdutoDto?> AtualizarAsync(int id, ProdutoAtualizacaoDto dto);
    Task<bool> DeletarAsync(int id);
}
