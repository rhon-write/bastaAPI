using bastaAPI.Data;
using bastaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubTasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public SubTasksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubTask>>> GetSubTasks()
    {
        return await _context.SubTasks.Include(s => s.ToDoItem).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubTask>> GetSubTask(int id)
    {
        var subTask = await _context.SubTasks
            .Include(s => s.ToDoItem)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (subTask == null) return NotFound();
        return subTask;
    }

    [HttpPost]
    public async Task<ActionResult<SubTask>> PostSubTask(SubTask subTask)
    {
        _context.SubTasks.Add(subTask);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSubTask), new { id = subTask.Id }, subTask);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutSubTask(int id, SubTask subTask)
    {
        if (id != subTask.Id) return BadRequest();

        _context.Entry(subTask).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubTask(int id)
    {
        var subTask = await _context.SubTasks.FindAsync(id);
        if (subTask == null) return NotFound();

        _context.SubTasks.Remove(subTask);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
