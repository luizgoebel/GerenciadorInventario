using GerenciadorInventario.WEB.Clients.Interface;
using GerenciadorInventario.WEB.Dtos;
using GerenciadorInventario.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorInventario.WEB.Controllers;

public class ProdutoController : Controller
{
    private readonly IProdutoApiClient _client;
    public ProdutoController(IProdutoApiClient client) => _client = client;

    public IActionResult Index() => View(new ProdutoListVm());

    [HttpGet]
    public async Task<IActionResult> Tabela(string? filtro, int page = 1, int pageSize = 10)
    {
        var itens = await _client.GetTodosAsync();
        if (!string.IsNullOrWhiteSpace(filtro))
            itens = itens.Where(p => p.Nome.Contains(filtro, StringComparison.OrdinalIgnoreCase));
        var total = itens.Count();
        var pageItems = itens.Skip((page - 1) * pageSize).Take(pageSize);
        var vm = new ProdutoListVm
        {
            Filtro = filtro,
            Dados = new PagedResult<ProdutoDto> { Items = pageItems, Page = page, PageSize = pageSize, TotalItems = total }
        };
        return PartialView("_Tabela", vm);
    }

    [HttpGet]
    public IActionResult Criar() => PartialView("_Editar", new ProdutoEditVm());

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] ProdutoCriacaoDto dto)
    {
        var criado = await _client.CriarAsync(dto);
        return Ok(criado);
    }

    [HttpGet]
    public async Task<IActionResult> Editar(int id)
    {
        var produto = await _client.GetByIdAsync(id);
        if (produto == null) return NotFound();
        return PartialView("_Editar", new ProdutoEditVm
        {
            Id = produto.Id,
            Atualizacao = new ProdutoAtualizacaoDto { Nome = produto.Nome, Descricao = produto.Descricao, Preco = produto.Preco }
        });
    }

    [HttpPost]
    public async Task<IActionResult> Editar(int id, [FromBody] ProdutoAtualizacaoDto dto)
    {
        var atualizado = await _client.AtualizarAsync(id, dto);
        return atualizado == null ? NotFound() : Ok(atualizado);
    }

    [HttpGet]
    public async Task<IActionResult> Visualizar(int id)
    {
        var produto = await _client.GetByIdAsync(id);
        if (produto == null) return NotFound();
        return PartialView("_Visualizar", produto);
    }

    [HttpPost]
    public async Task<IActionResult> Excluir(int id)
    {
        var ok = await _client.DeletarAsync(id);
        return ok ? Ok() : NotFound();
    }
}
