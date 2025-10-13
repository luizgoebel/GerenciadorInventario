using GerenciadorInventario.PedidoAPI.Context;
using GerenciadorInventario.PedidoAPI.Models;
using GerenciadorInventario.PedidoAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorInventario.PedidoAPI.Repository;

public class PedidoRepository : IPedidoRepository
{
    private readonly PedidoDbContext _ctx;
    public PedidoRepository(PedidoDbContext ctx) => _ctx = ctx;

    public async Task AddAsync(Pedido pedido)
    {
        _ctx.Pedidos.Add(pedido);
        await _ctx.SaveChangesAsync();
    }

    public async Task<IEnumerable<Pedido>> GetAllAsync()
    {
        return await _ctx.Pedidos
            .AsNoTracking()
            .Include(p => p.Itens)
            .OrderByDescending(p => p.Id)
            .ToListAsync();
    }

    public async Task<Pedido?> GetByIdWithItemsAsync(int id)
    {
        return await _ctx.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdateAsync(Pedido pedido)
    {
        _ctx.Pedidos.Update(pedido);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(Pedido pedido)
    {
        _ctx.Pedidos.Remove(pedido);
        await _ctx.SaveChangesAsync();
    }
}
