using GerenciadorInventario.WEB.Dtos;

namespace GerenciadorInventario.WEB.Clients.Interface;

public interface IReciboApiClient
{
    Task<ReciboDto?> GetPorFaturaAsync(int faturaId, CancellationToken ct = default);
    Task<ReciboDto?> GetPorIdAsync(int id, CancellationToken ct = default);
    Task<bool> EmitirAsync(int faturaId, string numeroFatura, decimal valorTotal, CancellationToken ct = default);
}
