using GerenciadorInventario.FaturamentoAPI.Models;

namespace GerenciadorInventario.FaturamentoAPI.Clients.Interface;

public interface IPedidoConsultaClient
{
    Task<PedidoResumo?> ObterPedidoAsync(int pedidoId);
}
