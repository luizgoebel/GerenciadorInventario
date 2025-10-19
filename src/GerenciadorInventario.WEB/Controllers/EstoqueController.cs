using GerenciadorInventario.WEB.Clients.Interface;
using GerenciadorInventario.WEB.Dtos;
using GerenciadorInventario.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorInventario.WEB.Controllers;

public class EstoqueController : Controller
{
    private readonly IEstoqueApiClient _client;
    private readonly IProdutoApiClient _produtoClient;
    public EstoqueController(IEstoqueApiClient client, IProdutoApiClient produtoClient)
    {
        _client = client;
        _produtoClient = produtoClient;
    }

    public IActionResult Index() => View(new EstoqueVm());

    [HttpGet]
    public async Task<IActionResult> Tabela(string? filtro, int page = 1, int pageSize = 10)
    {
        var produtos = (await _produtoClient.GetTodosAsync()).ToList();
        var itens = new List<EstoqueListItemVm>();
        foreach (var p in produtos)
        {
            var e = await _client.GetPorProdutoAsync(p.Id);
            if (e != null) itens.Add(new EstoqueListItemVm { ProdutoId = p.Id, ProdutoNome = p.Nome, Quantidade = e.Quantidade });
        }
        if (!string.IsNullOrWhiteSpace(filtro))
        {
            var f = filtro.Trim().ToLowerInvariant();
            itens = itens.Where(x => x.ProdutoNome.ToLowerInvariant().Contains(f) || x.Quantidade.ToString().Contains(f)).ToList();
        }
        var total = itens.Count;
        var pageItems = itens.Skip((page - 1) * pageSize).Take(pageSize);
        var vm = new EstoqueVm
        {
            Dados = new PagedResult<EstoqueListItemVm> { Items = pageItems, Page = page, PageSize = pageSize, TotalItems = total }
        };
        return PartialView("_Tabela", vm);
    }

    [HttpGet]
    public async Task<IActionResult> MovimentoEntrada(int? produtoId)
    {
        var produtos = (await _produtoClient.GetTodosAsync()).ToList();
        return PartialView("_Movimento", new MovimentoEstoqueVm
        {
            Movimento = new MovimentoEstoqueDto { ProdutoId = produtoId ?? 0 },
            Produtos = produtos.ToList(),
            LockProduct = produtoId.HasValue && produtoId.Value > 0
        });
    }

    [HttpPost]
    public async Task<IActionResult> MovimentoEntrada([FromBody] MovimentoEstoqueDto dto)
        => (await _client.EntradaAsync(dto)) ? Ok() : BadRequest();

    [HttpGet]
    public async Task<IActionResult> MovimentoSaida(int? produtoId)
    {
        var produtos = (await _produtoClient.GetTodosAsync()).ToList();
        return PartialView("_Movimento", new MovimentoEstoqueVm
        {
            Movimento = new MovimentoEstoqueDto { ProdutoId = produtoId ?? 0 },
            Produtos = produtos.ToList(),
            LockProduct = produtoId.HasValue && produtoId.Value > 0
        });
    }

    [HttpPost]
    public async Task<IActionResult> MovimentoSaida([FromBody] MovimentoEstoqueDto dto)
        => (await _client.SaidaAsync(dto)) ? Ok() : BadRequest();

    [HttpGet]
    public async Task<IActionResult> Visualizar(int produtoId)
    {
        var prod = await _produtoClient.GetByIdAsync(produtoId);
        var dto = await _client.GetPorProdutoAsync(produtoId);
        if (dto == null || prod == null) return NotFound();
        return PartialView("_Visualizar", new EstoqueDetalheVm { ProdutoId = produtoId, ProdutoNome = prod.Nome, Quantidade = dto.Quantidade });
    }
}
