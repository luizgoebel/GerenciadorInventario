using GerenciadorInventario.WEB.Dtos;

namespace GerenciadorInventario.WEB.ViewModels;

public class ProdutoListVm
{
    public string? Filtro { get; set; }
    public PagedResult<ProdutoDto> Dados { get; set; } = new();
}

public class ProdutoEditVm
{
    public int? Id { get; set; }
    public ProdutoCriacaoDto Novo { get; set; } = new();
    public ProdutoAtualizacaoDto Atualizacao { get; set; } = new();
}
