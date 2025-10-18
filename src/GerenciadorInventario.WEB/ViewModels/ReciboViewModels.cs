using GerenciadorInventario.WEB.Dtos;

namespace GerenciadorInventario.WEB.ViewModels;

public class ReciboVm
{
    public int? FaturaIdFiltro { get; set; }
    public PagedResult<ReciboDto> Dados { get; set; } = new();
}
