using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PR3_SecureAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PR3_SecureAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class IncidentsController : ControllerBase
    {
        private readonly IncidentContext _context;

        public IncidentsController(IncidentContext context)
        {
            _context = context;
        }

        // GET: api/Etablissements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incident>>> GetIncident()
        {
            return await _context.Incident.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Incident>> GetIncident(int id)
        {
            var incident = await _context.Incident.FindAsync(id);

            if (incident == null)
            {
                return NotFound();
            }

            return incident;
        }

        // PUT: api/Etablissements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncident(int id, Incident incident)
        {
            if (id != incident.Id)
            {
                return BadRequest();
            }

            _context.Entry(incident).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtablissementExists(id))
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

        // POST: api/Etablissements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Incident>> PostIncident(Incident incident)
        {
            _context.Incident.Add(incident);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncident", new { id = incident.Id }, incident);
        }

        // DELETE: api/Etablissements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncident(int id)
        {
            var incident = await _context.Incident.FindAsync(id);
            if (incident == null)
            {
                return NotFound();
            }

            _context.Incident.Remove(incident);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EtablissementExists(int id)
        {
            return _context.Incident.Any(e => e.Id == id);
        }
    }
}
