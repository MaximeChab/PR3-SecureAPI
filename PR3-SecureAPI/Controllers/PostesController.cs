using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PR3_SecureAPI.Models;

namespace PR3_SecureAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PostesController : ControllerBase
    {
        private readonly PosteContext _context;

        public PostesController(PosteContext context)
        {
            _context = context;
        }

        // GET: api/Postes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Poste>>> GetPoste()
        {
            return await _context.Poste.ToListAsync();
        }

        // GET: api/Postes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Poste>> GetPoste(int id)
        {
            var poste = await _context.Poste.FindAsync(id);

            if (poste == null)
            {
                return NotFound();
            }

            return poste;
        }

        [HttpGet("ByMacAdress/{macAdress}")]
        public async Task<ActionResult<Poste>> GetPosteByMacAdress(string macAdress)
        {
            var poste = await _context.Poste.FirstOrDefaultAsync(p => p.MacAdress == macAdress);

            if (poste == null)
            {
                return NotFound();
            }

            return poste;
        }


        // PUT: api/Postes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoste(int id, Poste poste)
        {
            if (id != poste.Id)
            {
                return BadRequest();
            }

            _context.Entry(poste).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosteExists(id))
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

        // POST: api/Postes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Poste>> PostPoste(Poste poste)
        {
            _context.Poste.Add(poste);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPoste", new { id = poste.Id }, poste);
        }

        // DELETE: api/Postes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoste(int id)
        {
            var poste = await _context.Poste.FindAsync(id);
            if (poste == null)
            {
                return NotFound();
            }

            _context.Poste.Remove(poste);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PosteExists(int id)
        {
            return _context.Poste.Any(e => e.Id == id);
        }
    }
}
