using AutoMapper;
using GerenciadorInventario.ProdutoAPI.Clients.Interface;
using GerenciadorInventario.ProdutoAPI.Dto;
using GerenciadorInventario.ProdutoAPI.Models;
using GerenciadorInventario.ProdutoAPI.Repository.Interface;
using GerenciadorInventario.ProdutoAPI.Service.Interface;
using Shared.Api.Exceptions;

namespace GerenciadorInventario.ProdutoAPI.Service;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repo;
    private readonly IMapper _mapper;
    private readonly IEstoqueClient _estoqueClient;

    public ProdutoService(IProdutoRepository repo, IMapper mapper, IEstoqueClient estoqueClient)
    {
        this._repo = repo;
        this._mapper = mapper;
        this._estoqueClient = estoqueClient;
    }

    public async Task<ProdutoDto> CriarProdutoAsync(ProdutoCriacaoDto dto)
    {
        Produto produto = this._mapper.Map<Produto>(dto);
        produto.Validar();

        await this._repo.AddAsync(produto);
        await this._estoqueClient.CriarEstoqueInicialAsync(produto.Id);

        return this._mapper.Map<ProdutoDto>(produto);
    }

    public async Task<ProdutoDto?> GetProdutoByIdAsync(int id)
    {
        Produto? produto = await this._repo.GetByIdAsync(id);
        return produto == null ? null : this._mapper.Map<ProdutoDto>(produto);
    }

    public async Task<IEnumerable<ProdutoDto>> GetTodosAsync()
    {
        IEnumerable<Produto> produtos = await this._repo.GetAllAsync();
        return produtos.Select(this._mapper.Map<ProdutoDto>);
    }

    public async Task<ProdutoDto?> AtualizarAsync(int id, ProdutoAtualizacaoDto dto)
    {
        Produto produto = await this._repo.GetByIdAsync(id) ??
            throw new ServiceException("Ocorreu um erro no servidor.");

        produto.Validar();
        produto.Alterar(dto.Nome, dto.Preco, dto.Descricao);

        await this._repo.UpdateAsync(produto);
        return this._mapper.Map<ProdutoDto>(produto);
    }

    public async Task<bool> DeletarAsync(int id)
    {
        Produto? produto = await this._repo.GetByIdAsync(id) ??
            throw new ServiceException("Produto não encontrado.");

        await this._repo.DeleteAsync(produto);
        return true;
    }
}
