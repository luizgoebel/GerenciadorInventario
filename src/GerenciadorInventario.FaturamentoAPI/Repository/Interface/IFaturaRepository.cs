using GerenciadorInventario.FaturamentoAPI.Models;

namespace GerenciadorInventario.Repository.Interface;

public interface IFaturaRepository
{
    Task AddAsync(Fatura fatura);
    Task<Fatura?> GetByIdAsync(int id);
    Task<Fatura?> GetByPedidoIdAsync(int pedidoId);
    Task UpdateAsync(Fatura fatura);
}
