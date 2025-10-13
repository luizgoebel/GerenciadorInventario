namespace GerenciadorInventario.ProdutoAPI.Clients.Interface;

public interface IEstoqueClient
{
    Task CriarEstoqueInicialAsync(int produtoId);
}
