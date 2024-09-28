using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class TareasController : ControllerBase
{
    private readonly AppDbContext _context;

    public TareasController(AppDbContext context)
    {
        _context = context;
    }

    // Listar todas las tareas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tarea>>> GetTareas()
    {
        return await _context.Tareas.ToListAsync();
    }

    // Crear una nueva tarea
    [HttpPost]
    public async Task<ActionResult<Tarea>> CreateTarea([FromBody] Tarea tarea)
    {
        if (tarea == null)
        {
            return BadRequest(new { message = "Los datos de la tarea no son válidos." });
        }

        _context.Tareas.Add(tarea);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTareas), new { id = tarea.Id }, tarea); // Devuelve la tarea creada
    }
}
