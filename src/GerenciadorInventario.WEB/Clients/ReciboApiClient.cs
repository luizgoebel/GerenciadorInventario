using System.Net.Http.Json;
using GerenciadorInventario.WEB.Clients.Interface;
using GerenciadorInventario.WEB.Dtos;

namespace GerenciadorInventario.WEB.Clients;

public class ReciboApiClient : IReciboApiClient
{
    private readonly HttpClient _http;
    private readonly ILogger<ReciboApiClient> _logger;

    public ReciboApiClient(HttpClient http, ILogger<ReciboApiClient> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<ReciboDto?> GetPorFaturaAsync(int faturaId, CancellationToken ct = default)
        => await _http.GetFromJsonAsync<ReciboDto>($"api/recibo/fatura/{faturaId}", ct);

    public async Task<ReciboDto?> GetPorIdAsync(int id, CancellationToken ct = default)
        => await _http.GetFromJsonAsync<ReciboDto>($"api/recibo/{id}", ct);
}
