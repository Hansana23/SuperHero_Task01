using Microsoft.EntityFrameworkCore;

namespace SuperHero.Models
{
    public class SuperHeroDBContext : DbContext
    {
        public SuperHeroDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Hero> SuperHeros { get; set; } = null!;
    }

}
