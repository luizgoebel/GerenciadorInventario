using GerenciadorInventario.WEB.Dtos;

namespace GerenciadorInventario.WEB.ViewModels;

public class EstoqueVm
{
    public int? ProdutoIdFiltro { get; set; }
    public PagedResult<EstoqueDto> Dados { get; set; } = new();
}

public class MovimentoEstoqueVm
{
    public MovimentoEstoqueDto Movimento { get; set; } = new();
}
