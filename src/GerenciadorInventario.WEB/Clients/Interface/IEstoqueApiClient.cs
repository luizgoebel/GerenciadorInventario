using GerenciadorInventario.WEB.Dtos;

namespace GerenciadorInventario.WEB.Clients.Interface;

public interface IEstoqueApiClient
{
    Task<EstoqueDto?> GetPorProdutoAsync(int produtoId, CancellationToken ct = default);
    Task<bool> InicializarAsync(int produtoId, CancellationToken ct = default);
    Task<bool> EntradaAsync(MovimentoEstoqueDto dto, CancellationToken ct = default);
    Task<bool> SaidaAsync(MovimentoEstoqueDto dto, CancellationToken ct = default);
}
