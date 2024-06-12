using Microsoft.EntityFrameworkCore;

namespace PR3_SecureAPI.Models 
{ 
    public class SalleContext : DbContext
{
    public SalleContext(DbContextOptions<SalleContext> options) : base(options)
    {
    }

    public DbSet<Salle> Salle { get; set; } = null!;

}
}
