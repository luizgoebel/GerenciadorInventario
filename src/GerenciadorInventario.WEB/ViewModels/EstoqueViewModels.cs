using GerenciadorInventario.WEB.Dtos;

namespace GerenciadorInventario.WEB.ViewModels;

public class EstoqueListItemVm
{
    public int ProdutoId { get; set; }
    public string ProdutoNome { get; set; } = string.Empty;
    public int Quantidade { get; set; }
}

public class EstoqueVm
{
    public int? ProdutoIdFiltro { get; set; }
    public PagedResult<EstoqueListItemVm> Dados { get; set; } = new();
}

public class MovimentoEstoqueVm
{
    public MovimentoEstoqueDto Movimento { get; set; } = new();
    public List<ProdutoDto> Produtos { get; set; } = new();
}

public class EstoqueDetalheVm
{
    public int ProdutoId { get; set; }
    public string ProdutoNome { get; set; } = string.Empty;
    public int Quantidade { get; set; }
}
