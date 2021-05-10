using System;
using System.Linq;
using System.Threading.Tasks;

namespace ZettelWirtschaft.Data
{
    public static class DataSeed
    {
        public static async Task SeedData(ZettelDbContext dbContext)
        {
            if (dbContext.Zettels.Any())
                return;

            await dbContext.Zettels.AddRangeAsync(
                new ZettelData() { Id = Guid.NewGuid(), Title = "Erster Zettel", Content = "Ein Zettel von solcher Güte, dass es nicht mehr normal ist.\n\nEhrlich." },
                new ZettelData() { Id = Guid.NewGuid(), Title = "Zweiter Zettel", Content = "Dieser Zettel...\n\n\nist gar nicht mal so heftig" }
            );
            await dbContext.SaveChangesAsync();

        }
    }
}