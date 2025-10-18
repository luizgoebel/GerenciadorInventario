using System.Net.Http.Json;
using GerenciadorInventario.WEB.Clients.Interface;
using GerenciadorInventario.WEB.Dtos;

namespace GerenciadorInventario.WEB.Clients;

public class PedidoApiClient : IPedidoApiClient
{
    private readonly HttpClient _http;
    private readonly ILogger<PedidoApiClient> _logger;

    public PedidoApiClient(HttpClient http, ILogger<PedidoApiClient> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<IEnumerable<PedidoDto>> GetTodosAsync(CancellationToken ct = default)
        => await _http.GetFromJsonAsync<IEnumerable<PedidoDto>>("api/pedido", ct) ?? Enumerable.Empty<PedidoDto>();

    public async Task<PedidoDto?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _http.GetFromJsonAsync<PedidoDto>($"api/pedido/{id}", ct);

    public async Task<PedidoDto> CriarAsync(PedidoCriacaoDto dto, CancellationToken ct = default)
    {
        var resp = await _http.PostAsJsonAsync("api/pedido/criar", dto, ct);
        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<PedidoDto>(cancellationToken: ct))!;
    }

    public async Task<bool> ConfirmarAsync(int id, CancellationToken ct = default)
        => (await _http.PostAsync($"api/pedido/{id}/confirmar", null, ct)).IsSuccessStatusCode;

    public async Task<bool> CancelarAsync(int id, CancellationToken ct = default)
        => (await _http.PutAsync($"api/pedido/{id}/cancelar", null, ct)).IsSuccessStatusCode;
}
