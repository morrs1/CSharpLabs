using CSharpLabs.DB;
using CSharpLabs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSharpLabs.Controllers;

[Route("api/animal")]
[ApiController]
public class AnimalsController(AppDbContext context) : ControllerBase
{
    // GET: api/Animals
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
    {
        return await context.Animal.ToListAsync();
    }

    // GET: api/Animals/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Animal>> GetAnimal(int id)
    {
        var animal = await context.Animal.FindAsync(id);

        if (animal == null)
        {
            return NotFound();
        }

        return animal;
    }

    [HttpPost]
    public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        context.Animal.Add(animal);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAnimal(int id, Animal animal)
    {
        if (id != animal.Id)
        {
            return BadRequest("ID в пути и теле запроса не совпадают");
        }

        if (!ModelState.IsValid) 
        {
            return BadRequest(ModelState);
        }

        context.Entry(animal).State = EntityState.Modified;
    
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AnimalExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Animals/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
        var animal = await context.Animal.FindAsync(id);
        if (animal == null)
        {
            return NotFound();
        }

        context.Animal.Remove(animal);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool AnimalExists(int id)
    {
        return context.Animal.Any(e => e.Id == id);
    }
}