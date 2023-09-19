using ABP_Backend.Data.Entities;
using System.Text.Json;

namespace ABP_Backend.Data.DB
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDBContext context)
        {
            if (!context.Genre.Any())
            {
                var genressData = File.ReadAllText("Data/DB/SeedDB/genres.json");
                var genres = JsonSerializer.Deserialize<List<Genre>>(genressData);
                context.Genre.AddRange(genres);
            }

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}
