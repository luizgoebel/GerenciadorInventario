using GerenciadorInventario.PedidoAPI.Dto;
using GerenciadorInventario.PedidoAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorInventario.PedidoAPI.Controllers;

[ApiController]
[Route("api/pedido")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _service;

    public PedidoController(IPedidoService service)
    {
        this._service = service;
    }

    [HttpPost("criar")]
    public async Task<IActionResult> Post([FromBody] PedidoCriacaoDto dto)
    {
        PedidoDto? pedido = await this._service.CriarAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedido);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        PedidoDto? pedido = await this._service.GetByIdAsync(id);
        return Ok(pedido);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<PedidoDto>? itens = await this._service.GetTodosAsync();
        return Ok(itens);
    }

    [HttpPut("{id:int}/cancelar")]
    public async Task<IActionResult> Cancelar(int id)
    {
        bool pedidoOk = await this._service.CancelarAsync(id);
        return Ok(pedidoOk);
    }
}
