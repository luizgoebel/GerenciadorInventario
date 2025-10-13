using GerenciadorInventario.EstoqueAPI.Dto;

namespace GerenciadorInventario.EstoqueAPI.Service.Interface;

public interface IEstoqueService
{
    Task<EstoqueDto?> GetPorProdutoIdAsync(int produtoId);
    Task<bool> EntradaAsync(MovimentoEstoqueDto dto);
    Task<bool> SaidaAsync(MovimentoEstoqueDto dto);
    Task<bool> CriarInicialSeNaoExisteAsync(int produtoId);
}
