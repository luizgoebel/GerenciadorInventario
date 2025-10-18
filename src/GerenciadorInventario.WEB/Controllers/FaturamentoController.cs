using GerenciadorInventario.WEB.Clients.Interface;
using GerenciadorInventario.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorInventario.WEB.Controllers;

public class FaturamentoController : Controller
{
    private readonly IFaturamentoApiClient _client;
    public FaturamentoController(IFaturamentoApiClient client)
    {
        _client = client;
    }

    public IActionResult Index() => View(new FaturamentoVm());

    [HttpGet]
    public async Task<IActionResult> Tabela(int? pedidoId, int page = 1, int pageSize = 10)
    {
        // placeholder until service paging is implemented
        var single = pedidoId.HasValue ? await _client.GetPorPedidoAsync(pedidoId.Value) : null;
        var itens = new List<GerenciadorInventario.WEB.Dtos.FaturaDto>();
        if (single != null) itens.Add(single);
        var total = itens.Count;
        var pageItems = itens.Skip((page - 1) * pageSize).Take(pageSize);
        var vm = new FaturamentoVm
        {
            PedidoIdFiltro = pedidoId,
            Dados = new ViewModels.PagedResult<GerenciadorInventario.WEB.Dtos.FaturaDto> { Items = pageItems, Page = page, PageSize = pageSize, TotalItems = total }
        };
        return PartialView("_Tabela", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Faturar(int pedidoId)
    {
        var res = await _client.FaturarAsync(pedidoId);
        return Ok(res);
    }

    [HttpGet]
    public async Task<IActionResult> Visualizar(int id)
    {
        var dto = await _client.GetPorIdAsync(id);
        if (dto == null) return NotFound();
        return PartialView("_Visualizar", dto);
    }
}
