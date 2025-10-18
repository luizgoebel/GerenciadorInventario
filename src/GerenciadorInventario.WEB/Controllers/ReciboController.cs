using GerenciadorInventario.WEB.Clients.Interface;
using GerenciadorInventario.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorInventario.WEB.Controllers;

public class ReciboController : Controller
{
    private readonly IReciboApiClient _client;
    public ReciboController(IReciboApiClient client) => _client = client;

    public IActionResult Index() => View(new ReciboVm());

    [HttpGet]
    public async Task<IActionResult> Tabela(int? faturaId, int page = 1, int pageSize = 10)
    {
        var single = faturaId.HasValue ? await _client.GetPorFaturaAsync(faturaId.Value) : null;
        var itens = new List<GerenciadorInventario.WEB.Dtos.ReciboDto>();
        if (single != null) itens.Add(single);
        var total = itens.Count;
        var pageItems = itens.Skip((page - 1) * pageSize).Take(pageSize);
        var vm = new ReciboVm
        {
            FaturaIdFiltro = faturaId,
            Dados = new ViewModels.PagedResult<GerenciadorInventario.WEB.Dtos.ReciboDto> { Items = pageItems, Page = page, PageSize = pageSize, TotalItems = total }
        };
        return PartialView("_Tabela", vm);
    }

    [HttpGet]
    public async Task<IActionResult> Visualizar(int id)
    {
        var dto = await _client.GetPorIdAsync(id);
        if (dto == null) return NotFound();
        return PartialView("_Visualizar", dto);
    }
}
