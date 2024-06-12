using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace PR3_SecureAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public ActionResult Login()
        {
            string token = this.GenerateToken("user");
            var obj = new { token };
            return Ok(obj);
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
