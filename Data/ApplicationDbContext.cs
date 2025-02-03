using Microsoft.EntityFrameworkCore;

namespace HealthCommunitiesCheck2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        
        //public DbSet<YourEntity> YourEntities { get; set; }
    }
}
