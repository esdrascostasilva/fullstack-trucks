using Backend.API.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModeloController : ControllerBase
{
    private readonly CaminhaoDbContext _context;

    public ModeloController(CaminhaoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var modelos = await _context.Modelos.ToListAsync();
        return Ok(modelos);
    }
}
