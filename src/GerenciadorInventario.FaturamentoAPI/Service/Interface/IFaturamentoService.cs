using GerenciadorInventario.FaturamentoAPI.Dto;

namespace GerenciadorInventario.FaturamentoAPI.Service.Interface;

public interface IFaturamentoService
{
    Task<FaturaCriacaoResultadoDto> FaturarPedidoAsync(int pedidoId);
    Task<FaturaDto?> ObterPorIdAsync(int id);
    Task<FaturaDto?> ObterPorPedidoAsync(int pedidoId);
}
