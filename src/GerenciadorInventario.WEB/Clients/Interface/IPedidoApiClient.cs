using GerenciadorInventario.WEB.Dtos;

namespace GerenciadorInventario.WEB.Clients.Interface;

public interface IPedidoApiClient
{
    Task<IEnumerable<PedidoDto>> GetTodosAsync(CancellationToken ct = default);
    Task<PedidoDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<PedidoDto> CriarAsync(PedidoCriacaoDto dto, CancellationToken ct = default);
    Task<bool> ConfirmarAsync(int id, CancellationToken ct = default);
    Task<bool> CancelarAsync(int id, CancellationToken ct = default);
}
