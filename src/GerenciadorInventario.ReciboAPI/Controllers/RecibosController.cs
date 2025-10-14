using GerenciadorInventario.ReciboAPI.Dto;
using GerenciadorInventario.ReciboAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Exceptions;

namespace GerenciadorInventario.ReciboAPI.Controllers;

[ApiController]
[Route("api/recibo")]
public class RecibosController : ControllerBase
{
    private readonly IReciboService _service;

    public RecibosController(IReciboService service)
    {
        _service = service;
    }

    [HttpPost("emissao")]
    public async Task<IActionResult> Emitir([FromBody] EmissaoReciboDto emissaoReciboDto)
    {
        try
        {
            ReciboDto reciboDto =
                await _service.GerarPorFaturaAsync(
                    emissaoReciboDto.FaturaId,
                    emissaoReciboDto.NumeroFatura,
                    emissaoReciboDto.ValorTotal);

            return Ok(reciboDto);
        }
        catch (ServiceException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
             throw new(message: ex.Message);
        }
    }

    [HttpGet("fatura/{faturaId:int}")]
    public async Task<IActionResult> GetPorFatura(int faturaId)
    {
        ReciboDto? reciboDto = await this._service.ObterPorFaturaAsync(faturaId);
        return Ok(reciboDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        ReciboDto? reciboDto = await this._service.ObterPorIdAsync(id);
        return Ok(reciboDto);
    }
}
