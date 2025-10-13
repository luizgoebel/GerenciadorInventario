using AutoMapper;
using GerenciadorInventario.ReciboAPI.Dto;
using GerenciadorInventario.ReciboAPI.Repository.Interface;
using GerenciadorInventario.ReciboAPI.Service.Interface;
using Shared.Api.Exceptions;

namespace GerenciadorInventario.ReciboAPI.Service;

public class ReciboService : IReciboService
{
    private readonly IReciboRepository _repo;
    private readonly IMapper _mapper;

    public ReciboService(IReciboRepository repo, IMapper mapper)
    {
        this._repo = repo;
        this._mapper = mapper;
    }

    public async Task<ReciboDto> GerarPorFaturaAsync(int faturaId, string numeroFatura, decimal valorTotal)
    {
        Models.Recibo? reciboExiste = await this._repo.GetByFaturaIdAsync(faturaId);
        if (reciboExiste != null) throw new ServiceException("Recibo existente.");
        string numeroRecibo = GerarNumeroRecibo(faturaId, numeroFatura);
        Models.Recibo recibo = new(faturaId, numeroRecibo, valorTotal);
        recibo.Validar();
        await this._repo.AddAsync(recibo);
        return _mapper.Map<ReciboDto>(recibo);
    }

    public async Task<ReciboDto?> ObterPorFaturaAsync(int faturaId)
    {
        Models.Recibo? recibo = await this._repo.GetByFaturaIdAsync(faturaId) ??
            throw new ServiceException("Recibo não gerado.");
        return _mapper.Map<ReciboDto>(recibo);
    }

    public async Task<ReciboDto?> ObterPorIdAsync(int id)
    {
        Models.Recibo? recibo = await this._repo.GetByIdAsync(id) ??
            throw new ServiceException("Recibo não gerado.");
        return _mapper.Map<ReciboDto>(recibo);
    }

    private string GerarNumeroRecibo(int faturaId, string numeroFatura)
        => $"RC-{DateTime.UtcNow:yyyyMMdd}-{faturaId}-{numeroFatura.Split('-').Last()}";
}
