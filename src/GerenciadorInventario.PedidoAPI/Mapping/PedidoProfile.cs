using AutoMapper;
using GerenciadorInventario.PedidoAPI.Dto;
using GerenciadorInventario.PedidoAPI.Models;

namespace GerenciadorInventario.PedidoAPI.Mapping;

public class PedidoProfile : Profile
{
    public PedidoProfile()
    {
        CreateMap<PedidoItem, PedidoItemDto>()
            .ForMember(d => d.Subtotal, o => o.MapFrom(s => s.Subtotal));
        CreateMap<Pedido, PedidoDto>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
            .ForMember(d => d.Itens, o => o.MapFrom(s => s.Itens));
    }
}
