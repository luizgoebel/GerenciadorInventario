namespace GerenciadorInventario.WEB.Dtos;

public class PedidoCriacaoDto
{
    public List<PedidoItemCriacaoDto> Itens { get; set; } = new();
}

public class PedidoItemCriacaoDto
{
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}

public class PedidoDto
{
    public int Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public List<PedidoItemDto> Itens { get; set; } = new();
}

public class PedidoItemDto
{
    public int ProdutoId { get; set; }
    public string? ProdutoNome { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
