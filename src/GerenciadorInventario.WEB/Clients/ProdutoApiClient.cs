using System.Net.Http.Json;
using GerenciadorInventario.WEB.Clients.Interface;
using GerenciadorInventario.WEB.Dtos;

namespace GerenciadorInventario.WEB.Clients;

public class ProdutoApiClient : IProdutoApiClient
{
    private readonly HttpClient _http;
    private readonly ILogger<ProdutoApiClient> _logger;

    public ProdutoApiClient(HttpClient http, ILogger<ProdutoApiClient> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<IEnumerable<ProdutoDto>> GetTodosAsync(CancellationToken ct = default)
    {
        var result = await _http.GetFromJsonAsync<IEnumerable<ProdutoDto>>("api/produto", ct) ?? Enumerable.Empty<ProdutoDto>();
        return result;
    }

    public async Task<ProdutoDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<ProdutoDto>($"api/produto/{id}", ct);
    }

    public async Task<ProdutoDto> CriarAsync(ProdutoCriacaoDto dto, CancellationToken ct = default)
    {
        var resp = await _http.PostAsJsonAsync("api/produto/criar", dto, ct);
        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<ProdutoDto>(cancellationToken: ct))!;
    }

    public async Task<ProdutoDto?> AtualizarAsync(int id, ProdutoAtualizacaoDto dto, CancellationToken ct = default)
    {
        var resp = await _http.PutAsJsonAsync($"api/produto/{id}", dto, ct);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<ProdutoDto>(cancellationToken: ct);
    }

    public async Task<bool> DeletarAsync(int id, CancellationToken ct = default)
    {
        var resp = await _http.DeleteAsync($"api/produto/{id}", ct);
        return resp.IsSuccessStatusCode;
    }
}
