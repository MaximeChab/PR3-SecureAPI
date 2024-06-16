using Microsoft.EntityFrameworkCore;

namespace PR3_SecureAPI.Models
{
    public class IncidentContext : DbContext
    {
        public IncidentContext(DbContextOptions<IncidentContext> options) : base(options)
        {
        }

        public DbSet<Incident> Incident { get; set; } = null!;
    }
}
