using ABP_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABP_Backend.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AudioBook> AudioBook { get; set; }
    }
}
