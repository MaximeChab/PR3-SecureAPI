using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PR3_SecureAPI.Models;

namespace PR3_SecureAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SallesController : ControllerBase
    {
        private readonly SalleContext _context;

        public SallesController(SalleContext context)
        {
            _context = context;
        }

        // GET: api/ClasseItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salle>>> GetClasseItems()
        {
            return await _context.Salle.ToListAsync();
        }

        // GET: api/ClasseItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Salle>> GetClasseItem(int id)
        {
            var classeItem = await _context.Salle.FindAsync(id);

            if (classeItem == null)
            {
                return NotFound();
            }

            return classeItem;
        }

        // PUT: api/ClasseItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClasseItem(int id, Salle classeItem)
        {
            if (id != classeItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(classeItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClasseItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ClasseItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Salle>> PostClasseItem(Salle classeItem)
        {
            _context.Salle.Add(classeItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClasseItem", new { id = classeItem.Id }, classeItem);
        }

        // DELETE: api/ClasseItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClasseItem(int id)
        {
            var classeItem = await _context.Salle.FindAsync(id);
            if (classeItem == null)
            {
                return NotFound();
            }

            _context.Salle.Remove(classeItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClasseItemExists(int id)
        {
            return _context.Salle.Any(e => e.Id == id);
        }
    }
}
