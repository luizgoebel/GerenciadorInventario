using System.Net.Http.Json;
using GerenciadorInventario.WEB.Clients.Interface;
using GerenciadorInventario.WEB.Dtos;

namespace GerenciadorInventario.WEB.Clients;

public class FaturamentoApiClient : IFaturamentoApiClient
{
    private readonly HttpClient _http;
    private readonly ILogger<FaturamentoApiClient> _logger;

    public FaturamentoApiClient(HttpClient http, ILogger<FaturamentoApiClient> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<FaturaCriacaoResultadoDto> FaturarAsync(int pedidoId, CancellationToken ct = default)
    {
        var resp = await _http.PostAsync($"api/faturamento/{pedidoId}", null, ct);
        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<FaturaCriacaoResultadoDto>(cancellationToken: ct))!;
    }

    public async Task<FaturaDto?> GetPorPedidoAsync(int pedidoId, CancellationToken ct = default)
        => await _http.GetFromJsonAsync<FaturaDto>($"api/faturamento/pedido/{pedidoId}", ct);

    public async Task<FaturaDto?> GetPorIdAsync(int id, CancellationToken ct = default)
        => await _http.GetFromJsonAsync<FaturaDto>($"api/faturamento/{id}", ct);
}
