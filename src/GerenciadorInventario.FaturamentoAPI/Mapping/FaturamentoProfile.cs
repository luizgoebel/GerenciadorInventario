using AutoMapper;
using GerenciadorInventario.FaturamentoAPI.Dto;
using GerenciadorInventario.FaturamentoAPI.Models;

namespace GerenciadorInventario.FaturamentoAPI.Mapping;

public class FaturamentoProfile : Profile
{
    public FaturamentoProfile()
    {
        CreateMap<Fatura, FaturaDto>()
            .ConvertUsing(f => Map(f));

        CreateMap<Fatura, FaturaCriacaoResultadoDto>()
            .ConstructUsing(f => new FaturaCriacaoResultadoDto(f.Id, f.Numero, f.Total));
    }

    private static FaturaDto Map(Fatura f)
        => new(
            f.Id,
            f.Numero,
            f.PedidoId,
            f.DataEmissao,
            f.Total,
            f.Itens.Select(i => new FaturaItemDto(i.ProdutoId, i.Quantidade, i.PrecoUnitario, i.Subtotal)),
            f.Status.ToString());
}
