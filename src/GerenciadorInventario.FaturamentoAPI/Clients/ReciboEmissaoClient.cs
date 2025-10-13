using GerenciadorInventario.FaturamentoAPI.Clients.Interface;

namespace GerenciadorInventario.FaturamentoAPI.Clients;

public class ReciboEmissaoClient : IReciboEmissaoClient
{
    private readonly HttpClient _http;
    private readonly ILogger<ReciboEmissaoClient> _logger;

    public ReciboEmissaoClient(HttpClient http, ILogger<ReciboEmissaoClient> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<bool> EmitirReciboAsync(int faturaId, string numeroFatura, decimal valorTotal)
    {
        try
        {
            var resp = await _http.PostAsJsonAsync("api/recibo/emissao", new { faturaId, numeroFatura, valorTotal });
            return resp.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro emitindo recibo para fatura {FaturaId}", faturaId);
            return false;
        }
    }
}
