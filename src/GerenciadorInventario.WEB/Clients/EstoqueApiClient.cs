using System.Net.Http.Json;
using GerenciadorInventario.WEB.Clients.Interface;
using GerenciadorInventario.WEB.Dtos;

namespace GerenciadorInventario.WEB.Clients;

public class EstoqueApiClient : IEstoqueApiClient
{
    private readonly HttpClient _http;
    private readonly ILogger<EstoqueApiClient> _logger;

    public EstoqueApiClient(HttpClient http, ILogger<EstoqueApiClient> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<EstoqueDto?> GetPorProdutoAsync(int produtoId, CancellationToken ct = default)
        => await _http.GetFromJsonAsync<EstoqueDto>($"api/estoque/{produtoId}", ct);

    public async Task<bool> InicializarAsync(int produtoId, CancellationToken ct = default)
        => (await _http.PostAsync($"api/estoque/inicial/{produtoId}", null, ct)).IsSuccessStatusCode;

    public async Task<bool> EntradaAsync(MovimentoEstoqueDto dto, CancellationToken ct = default)
        => (await _http.PostAsJsonAsync("api/estoque/entrada", dto, ct)).IsSuccessStatusCode;

    public async Task<bool> SaidaAsync(MovimentoEstoqueDto dto, CancellationToken ct = default)
        => (await _http.PostAsJsonAsync("api/estoque/saida", dto, ct)).IsSuccessStatusCode;
}
