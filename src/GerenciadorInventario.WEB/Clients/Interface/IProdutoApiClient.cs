using GerenciadorInventario.WEB.Dtos;

namespace GerenciadorInventario.WEB.Clients.Interface;

public interface IProdutoApiClient
{
    Task<IEnumerable<ProdutoDto>> GetTodosAsync(CancellationToken ct = default);
    Task<ProdutoDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<ProdutoDto> CriarAsync(ProdutoCriacaoDto dto, CancellationToken ct = default);
    Task<ProdutoDto?> AtualizarAsync(int id, ProdutoAtualizacaoDto dto, CancellationToken ct = default);
    Task<bool> DeletarAsync(int id, CancellationToken ct = default);
}
