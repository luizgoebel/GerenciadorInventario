using GerenciadorInventario.PedidoAPI.Models;

namespace GerenciadorInventario.PedidoAPI.Repository.Interface;

public interface IPedidoRepository
{
    Task AddAsync(Pedido pedido);
    Task<Pedido?> GetByIdWithItemsAsync(int id);
    Task<IEnumerable<Pedido>> GetAllAsync();
    Task UpdateAsync(Pedido pedido);
    Task DeleteAsync(Pedido pedido);
}
