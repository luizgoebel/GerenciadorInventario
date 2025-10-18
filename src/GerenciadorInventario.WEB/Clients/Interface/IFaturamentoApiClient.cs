using GerenciadorInventario.WEB.Dtos;

namespace GerenciadorInventario.WEB.Clients.Interface;

public interface IFaturamentoApiClient
{
    Task<FaturaCriacaoResultadoDto> FaturarAsync(int pedidoId, CancellationToken ct = default);
    Task<FaturaDto?> GetPorPedidoAsync(int pedidoId, CancellationToken ct = default);
    Task<FaturaDto?> GetPorIdAsync(int id, CancellationToken ct = default);
}
