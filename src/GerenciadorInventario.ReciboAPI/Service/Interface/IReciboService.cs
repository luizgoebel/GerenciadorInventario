using GerenciadorInventario.ReciboAPI.Dto;

namespace GerenciadorInventario.ReciboAPI.Service.Interface;

public interface IReciboService
{
    Task<ReciboDto> GerarPorFaturaAsync(int faturaId, string numeroFatura, decimal valorTotal);
    Task<ReciboDto?> ObterPorFaturaAsync(int faturaId);
    Task<ReciboDto?> ObterPorIdAsync(int id);
}
