using GerenciadorInventario.WEB.Clients.Interface;
using GerenciadorInventario.WEB.Dtos;
using GerenciadorInventario.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorInventario.WEB.Controllers;

public class PedidoController : Controller
{
    private readonly IPedidoApiClient _client;
    private readonly IProdutoApiClient _produtoClient;
    private readonly IFaturamentoApiClient _fatClient;
    private readonly IReciboApiClient _reciboClient;

    public PedidoController(IPedidoApiClient client, IProdutoApiClient produtoClient, IFaturamentoApiClient fatClient, IReciboApiClient reciboClient)
    {
        _client = client;
        _produtoClient = produtoClient;
        _fatClient = fatClient;
        _reciboClient = reciboClient;
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
        var produtos = await _produtoClient.GetTodosAsync();
        return PartialView("_Editar", new PedidoEditVm { Produtos = produtos.ToList() });
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

    // Faturamento integrado no Pedido
    [HttpPost]
    public async Task<IActionResult> Faturar(int pedidoId)
    {
        var res = await _fatClient.FaturarAsync(pedidoId);
        return Ok(res);
    }

    [HttpGet]
    public async Task<IActionResult> VisualizarFatura(int pedidoId)
    {
        var dto = await _fatClient.GetPorPedidoAsync(pedidoId);
        if (dto == null) return NotFound();
        return PartialView("_FaturaVisualizar", dto);
    }

    // Recibo integrado no Pedido
    [HttpPost]
    public async Task<IActionResult> EmitirRecibo(int pedidoId)
    {
        var fatura = await _fatClient.GetPorPedidoAsync(pedidoId);
        if (fatura == null) return NotFound();
        var ok = await _reciboClient.EmitirAsync(fatura.Id, fatura.Numero, fatura.Total);
        return ok ? Ok() : BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> VisualizarRecibo(int pedidoId)
    {
        var fatura = await _fatClient.GetPorPedidoAsync(pedidoId);
        if (fatura == null) return NotFound();
        var recibo = await _reciboClient.GetPorFaturaAsync(fatura.Id);
        if (recibo == null) return NotFound();
        return PartialView("_ReciboVisualizar", recibo);
    }
}
