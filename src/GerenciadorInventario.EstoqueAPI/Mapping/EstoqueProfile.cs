using AutoMapper;
using GerenciadorInventario.EstoqueAPI.Dto;
using GerenciadorInventario.EstoqueAPI.Models;

namespace GerenciadorInventario.EstoqueAPI.Mapping;

public class EstoqueProfile : Profile
{
    public EstoqueProfile()
    {
        CreateMap<Estoque, EstoqueDto>();
    }
{
}
