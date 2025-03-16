using CSharpLabs.DB;
using CSharpLabs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/enclosure")]
[ApiController]
public class EnclosuresController(AppDbContext context) : ControllerBase
{
    // GET: api/Enclosures
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Enclosure>>> GetEnclosures()
    {
        return await context.Enclosure
            .Include(e => e.Animal)
            .ToListAsync();
    }

    // GET: api/Enclosures/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Enclosure>> GetEnclosure(int id)
    {
        var enclosure = await context.Enclosure
            .Include(e => e.Animal)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (enclosure == null)
        {
            return NotFound();
        }

        return enclosure;
    }

    // POST: api/Enclosures
    [HttpPost]
    public async Task<ActionResult<Enclosure>> PostEnclosure(Enclosure enclosure)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        context.Enclosure.Add(enclosure);
        await context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetEnclosure),
            new { id = enclosure.Id },
            enclosure);
    }

    // PUT: api/Enclosures/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutEnclosure(int id, Enclosure enclosure)
    {
        if (id != enclosure.Id)
        {
            return BadRequest("ID в URL и теле запроса не совпадают");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Обновляем связь с животным
        context.Entry(enclosure).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EnclosureExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // DELETE: api/Enclosures/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEnclosure(int id)
    {
        var enclosure = await context.Enclosure
            .Include(e => e.Animal) // Удаляем связанное животное
            .FirstOrDefaultAsync(e => e.Id == id);

        if (enclosure == null)
        {
            return NotFound();
        }

        context.Enclosure.Remove(enclosure);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool EnclosureExists(int id)
    {
        return context.Enclosure.Any(e => e.Id == id);
    }
}