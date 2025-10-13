using GerenciadorInventario.ReciboAPI.Context;
using GerenciadorInventario.ReciboAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorInventario.ReciboAPI.Repository;

public class ReciboRepository : IReciboRepository
{
    private readonly ReciboDbContext _ctx;

    public ReciboRepository(ReciboDbContext ctx) => _ctx = ctx;

    public async Task AddAsync(Models.Recibo recibo)
    {
        await _ctx.Recibos.AddAsync(recibo);
        await _ctx.SaveChangesAsync();
    }

    public Task<Models.Recibo?> GetByFaturaIdAsync(int faturaId)
        => _ctx.Recibos.AsNoTracking().FirstOrDefaultAsync(r => r.FaturaId == faturaId);

    public Task<Models.Recibo?> GetByIdAsync(int id)
        => _ctx.Recibos.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
}
