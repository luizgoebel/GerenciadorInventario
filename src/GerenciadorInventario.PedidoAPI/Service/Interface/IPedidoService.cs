using GerenciadorInventario.PedidoAPI.Dto;

namespace GerenciadorInventario.PedidoAPI.Service.Interface;

public interface IPedidoService
{
    Task<PedidoDto> CriarAsync(PedidoCriacaoDto dto);
    Task<PedidoDto?> GetByIdAsync(int id);
    Task<IEnumerable<PedidoDto>> GetTodosAsync();
    Task<bool> CancelarAsync(int id);
    Task<bool> ConfirmarAsync(int id);
}
