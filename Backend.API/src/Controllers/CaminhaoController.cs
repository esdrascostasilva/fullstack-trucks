using Backend.API.Models;
using Backend.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CaminhaoController : ControllerBase
{
    private readonly ICaminhaoService _caminhaoService;

    public CaminhaoController(ICaminhaoService caminhaoService)
    {
        _caminhaoService = caminhaoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Caminhao>>> GetAll()
    {
        var caminhoes = await _caminhaoService.GetAllAsync();

        return Ok(caminhoes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Caminhao>> GetById(int id)
    {
        try
        {
            var caminhao = await _caminhaoService.GetByIdAsync(id);

            return Ok(caminhao);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Caminhao>> Create(Caminhao caminhao)
    {
        var novoCaminhao = await _caminhaoService.PostAsync(caminhao);

        return CreatedAtAction(nameof(GetById), new { id = novoCaminhao.Id }, novoCaminhao);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Caminhao caminhao)
    {
        try
        {
            await _caminhaoService.PutAsync(id, caminhao);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _caminhaoService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

}
