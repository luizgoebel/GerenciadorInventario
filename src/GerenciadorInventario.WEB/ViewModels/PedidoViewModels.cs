using GerenciadorInventario.WEB.Dtos;

namespace GerenciadorInventario.WEB.ViewModels;

public class PedidoListVm
{
    public string? Filtro { get; set; }
    public PagedResult<PedidoDto> Dados { get; set; } = new();
}

public class PedidoEditVm
{
    public PedidoCriacaoDto Novo { get; set; } = new();
    public List<ProdutoDto> Produtos { get; set; } = new();
}
