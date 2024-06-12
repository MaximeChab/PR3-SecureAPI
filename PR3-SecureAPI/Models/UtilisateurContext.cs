using Microsoft.EntityFrameworkCore;

namespace PR3_SecureAPI.Models
{
    public class UtilisateurContext : DbContext
    {
        public UtilisateurContext(DbContextOptions<UtilisateurContext> options) : base(options)
        {
        }

        public DbSet<Utilisateur> Utilisateur { get; set; } = null!;

    }
}
