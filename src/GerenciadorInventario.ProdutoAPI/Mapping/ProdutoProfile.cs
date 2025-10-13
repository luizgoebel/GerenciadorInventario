using AutoMapper;
using GerenciadorInventario.ProdutoAPI.Dto;
using GerenciadorInventario.ProdutoAPI.Models;

namespace GerenciadorInventario.ProdutoAPI.Mapping;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<Produto, ProdutoDto>();
        CreateMap<ProdutoCriacaoDto, Produto>();
        CreateMap<ProdutoAtualizacaoDto, Produto>();
    }
}
