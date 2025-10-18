using GerenciadorInventario.WEB.Clients.Interface;
using GerenciadorInventario.WEB.Dtos;
using GerenciadorInventario.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorInventario.WEB.Controllers;

public class PedidoController : Controller
{
    private readonly IPedidoApiClient _client;
    private readonly IProdutoApiClient _produtoClient;
    public PedidoController(IPedidoApiClient client, IProdutoApiClient produtoClient)
    {
        _client = client;
        _produtoClient = produtoClient;
    }

    public IActionResult Index() => View(new PedidoListVm());

    [HttpGet]
    public async Task<IActionResult> Tabela(string? filtro, int page = 1, int pageSize = 10)
    {
        var itens = await _client.GetTodosAsync();
        if (!string.IsNullOrWhiteSpace(filtro))
            itens = itens.Where(p => p.Status.Contains(filtro, StringComparison.OrdinalIgnoreCase));
        var total = itens.Count();
        var pageItems = itens.Skip((page - 1) * pageSize).Take(pageSize);
        var vm = new PedidoListVm
        {
            Filtro = filtro,
            Dados = new PagedResult<PedidoDto> { Items = pageItems, Page = page, PageSize = pageSize, TotalItems = total }
        };
        return PartialView("_Tabela", vm);
    }

    [HttpGet]
    public async Task<IActionResult> Criar()
    {
        return PartialView("_Editar", new PedidoEditVm());
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] PedidoCriacaoDto dto)
    {
        var criado = await _client.CriarAsync(dto);
        return Ok(criado);
    }

    [HttpGet]
    public async Task<IActionResult> Visualizar(int id)
    {
        var pedido = await _client.GetByIdAsync(id);
        if (pedido == null) return NotFound();
        return PartialView("_Visualizar", pedido);
    }

    [HttpPost]
    public async Task<IActionResult> Confirmar(int id)
        => (await _client.ConfirmarAsync(id)) ? Ok() : BadRequest();

    [HttpPost]
    public async Task<IActionResult> Cancelar(int id)
        => (await _client.CancelarAsync(id)) ? Ok() : BadRequest();
}
