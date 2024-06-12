using Microsoft.EntityFrameworkCore;

namespace PR3_SecureAPI.Models
{
    public class EtablissementContext : DbContext
    {

        public EtablissementContext(DbContextOptions<EtablissementContext> options) : base(options)
        {
        }

        public DbSet<Etablissement> Etablissement { get; set; } = null!;
    }
}