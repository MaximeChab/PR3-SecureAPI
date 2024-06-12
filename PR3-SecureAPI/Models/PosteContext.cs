using Microsoft.EntityFrameworkCore;

namespace PR3_SecureAPI.Models
{
    public class PosteContext : DbContext
    {
        public PosteContext(DbContextOptions<PosteContext> options) : base(options)
        {
        }

        public DbSet<Poste> Poste { get; set; } = null!;
    }
}
