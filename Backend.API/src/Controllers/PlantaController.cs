using Backend.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.API;

[ApiController]
[Route("api/[controller]")]
public class PlantaController : ControllerBase
{
    private readonly CaminhaoDbContext _context;

    public PlantaController(CaminhaoDbContext dbContext)
    {
        _context = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var plantas = await _context.Plantas.ToListAsync();

        return Ok(plantas);
    }
}
