using AutoMapper;
using GerenciadorInventario.ReciboAPI.Dto;

namespace GerenciadorInventario.ReciboAPI.Mapping;

public class ReciboProfile : Profile
{
    public ReciboProfile()
    {
        CreateMap<Models.Recibo, ReciboDto>()
            .ConstructUsing(r => new ReciboDto(r.Id, r.Numero, r.FaturaId, r.DataEmissao, r.ValorTotal));
    }
}
