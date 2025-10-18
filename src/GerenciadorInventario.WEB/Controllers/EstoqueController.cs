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
    public async Task<IActionResult> Tabela(int? produtoId, int page = 1, int pageSize = 10)
    {
        var produtos = await _produtoClient.GetTodosAsync();
        var itens = new List<EstoqueDto>();
        foreach (var p in produtos)
        {
            var e = await _client.GetPorProdutoAsync(p.Id);
            if (e != null) itens.Add(e);
        }
        if (produtoId.HasValue)
            itens = itens.Where(x => x.ProdutoId == produtoId.Value).ToList();
        var total = itens.Count;
        var pageItems = itens.Skip((page - 1) * pageSize).Take(pageSize);
        var vm = new EstoqueVm
        {
            ProdutoIdFiltro = produtoId,
            Dados = new PagedResult<EstoqueDto> { Items = pageItems, Page = page, PageSize = pageSize, TotalItems = total }
        };
        return PartialView("_Tabela", vm);
    }

    [HttpGet]
    public IActionResult MovimentoEntrada(int produtoId) => PartialView("_Movimento", new MovimentoEstoqueVm
    {
        Movimento = new MovimentoEstoqueDto { ProdutoId = produtoId }
    });

    [HttpPost]
    public async Task<IActionResult> MovimentoEntrada([FromBody] MovimentoEstoqueDto dto)
        => (await _client.EntradaAsync(dto)) ? Ok() : BadRequest();

    [HttpGet]
    public IActionResult MovimentoSaida(int produtoId) => PartialView("_Movimento", new MovimentoEstoqueVm
    {
        Movimento = new MovimentoEstoqueDto { ProdutoId = produtoId }
    });

    [HttpPost]
    public async Task<IActionResult> MovimentoSaida([FromBody] MovimentoEstoqueDto dto)
        => (await _client.SaidaAsync(dto)) ? Ok() : BadRequest();

    [HttpGet]
    public async Task<IActionResult> Visualizar(int produtoId)
    {
        var dto = await _client.GetPorProdutoAsync(produtoId);
        if (dto == null) return NotFound();
        return PartialView("_Visualizar", dto);
    }
}
