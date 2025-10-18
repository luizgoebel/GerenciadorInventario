using GerenciadorInventario.WEB.Dtos;

namespace GerenciadorInventario.WEB.ViewModels;

public class FaturamentoVm
{
    public int? PedidoIdFiltro { get; set; }
    public PagedResult<FaturaDto> Dados { get; set; } = new();
}
