using GerenciadorInventario.ProdutoAPI.Clients.Interface;

namespace GerenciadorInventario.ProdutoAPI.Clients;

public class EstoqueClient : IEstoqueClient
{
    private readonly HttpClient _httpClient;
    public EstoqueClient(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    public async Task CriarEstoqueInicialAsync(int produtoId)
    {
        var estoqueInicial = new { ProdutoId = produtoId, Quantidade = 0 };
        try
        {
            var response = await this._httpClient.PostAsJsonAsync("api/estoque/entrada", estoqueInicial);
            response.EnsureSuccessStatusCode();
        }
        catch
        {
            // Logar falha de comunicação (omitted)
        }
    }
}
