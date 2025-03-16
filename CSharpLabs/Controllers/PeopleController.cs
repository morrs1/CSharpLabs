using CSharpLabs.DB;
using CSharpLabs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/people")]
[ApiController]
public class PeoplesController(AppDbContext context) : ControllerBase
{
    // GET: api/People
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
    {
        return await context.People.Include(p => p.Animals).ToListAsync();
    }

    // POST: api/People
    [HttpPost]
    public async Task<ActionResult<Person>> PostPerson(Person person)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        context.People.Add(person);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPeople), new { id = person.Id }, person);
    }

    // PUT: api/People/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson(int id, Person person)
    {
        if (id != person.Id)
        {
            return BadRequest("ID в URL и теле запроса не совпадают");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        context.Entry(person).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PersonExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }
    
    // DELETE: api/People/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        var person = await context.People
            .Include(p => p.Animals)  // Каскадное удаление животных
            .FirstOrDefaultAsync(p => p.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        context.People.Remove(person);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool PersonExists(int id)
    {
        return context.People.Any(e => e.Id == id);
    }
}