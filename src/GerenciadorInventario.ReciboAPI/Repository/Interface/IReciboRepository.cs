using GerenciadorInventario.ReciboAPI.Models;

namespace GerenciadorInventario.ReciboAPI.Repository.Interface;

public interface IReciboRepository
{
    Task AddAsync(Recibo recibo);
    Task<Recibo?> GetByFaturaIdAsync(int faturaId);
    Task<Recibo?> GetByIdAsync(int id);
}
