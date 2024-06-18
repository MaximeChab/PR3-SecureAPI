using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PR3_SecureAPI.Models;
using System.Text;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace PR3_SecureAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UtilisateursController : ControllerBase
    {
        private readonly UtilisateurContext _context;
        //private readonly string _secretKey;

        private readonly ILogger<UtilisateursController> _logger;


        public UtilisateursController(UtilisateurContext context)
        {
            _context = context;
        }

        // GET: api/Utilisateurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateur()
        {
            /*var utilisateurs = await _context.Utilisateur
            .Select(u => new {
                u.Id,
                u.Nom,
                u.Prenom,
                u.Role,
                // Include other properties you need
            })
            .ToListAsync();

            return Ok(utilisateurs);*/
           return await _context.Utilisateur.ToListAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateur(int id)
        {
            /*var utilisateur = await _context.Utilisateur
            .Where(u => u.Id == id)
            .Select(u => new {
                u.Id,
                u.Nom,
                u.Prenom,
                u.Role,
                // Include other properties you need
            })
            .FirstOrDefaultAsync();

            if (utilisateur == null)
            {
                return NotFound();
            }

            return Ok(utilisateur);*/
            var utilisateur = await _context.Utilisateur.FindAsync(id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (id != utilisateur.Id)
            {
                return BadRequest();
            }
            utilisateur.MotDePasse = HashString(utilisateur.MotDePasse);
            _context.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
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

        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<ActionResult<Utilisateur>> PostEtablissement(Utilisateur utilisateur)
        {
            utilisateur.MotDePasse = HashString(utilisateur.MotDePasse);
            _context.Utilisateur.Add(utilisateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtilisateur", new { id = utilisateur.Id }, utilisateur);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            login.MotDePasse = HashString(login.MotDePasse);
            var utilisateur = await _context.Utilisateur.FirstOrDefaultAsync(u => u.Login == login.Login && u.MotDePasse == login.MotDePasse);
            if (utilisateur == null)
            {
                return Unauthorized();
            }
            string token = this.GenerateToken(utilisateur.Login); // Using the username for the token
            var obj = new
            {
                token,
                Utilisateur = new
                {
                    utilisateur.Id,
                    utilisateur.Login,
                    utilisateur.Nom, // Assuming the user has a name field, add other fields as needed
                    utilisateur.Prenom, // Assuming the user has a first name field, add other fields as needed
                    utilisateur.Role
                }
            };
            return Ok(obj);

        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var utilisateur = await _context.Utilisateur.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            _context.Utilisateur.Remove(utilisateur);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool UtilisateurExists(int id)
        {
            return _context.Utilisateur.Any(e => e.Id == id);
        }

        public class LoginRequest
        {
            public string Login { get; set; }
            public string MotDePasse { get; set; }
        }

        static string HashString(string text)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private string GenerateToken(String user)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("96ZP0WzO5W3ZMWWVqZVhsUK0h3lChcdj96ZP0WzO5W3ZMWWVqZVhsUK0h3lChcdj"));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>() { new Claim("username", user) };

            JwtSecurityToken st = new JwtSecurityToken("3iL", "API test", claims, DateTime.UtcNow, DateTime.UtcNow.AddHours(1), credentials);

            return new JwtSecurityTokenHandler().WriteToken(st);
        }

    }
}
