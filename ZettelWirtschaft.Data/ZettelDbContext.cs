using Microsoft.EntityFrameworkCore;

namespace ZettelWirtschaft.Data
{
    public class ZettelDbContext : DbContext
    {
        public ZettelDbContext(DbContextOptions<ZettelDbContext> options) : base(options) { }
        public DbSet<ZettelData> Zettels { get; set; }
    }
}