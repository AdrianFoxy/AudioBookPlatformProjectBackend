using ABP_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace ABP_Backend.Data.DB
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Get all Entities that inherited from BaseEntity
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(BaseEntity).IsAssignableFrom(e.ClrType));

            // Set default for every entity
            foreach (var entityType in entityTypes)
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property("CreatedAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity(entityType.ClrType)
                    .Property("UpdatedAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            }

            // Many-to-Many Entity
            modelBuilder.Entity<AudioBook>()
                .HasMany(e => e.Author)
                .WithMany(e => e.AudioBook)
                .UsingEntity<AudioBookAuthor>();

            modelBuilder.Entity<AudioBook>()
                .HasMany(e => e.Genre)
                .WithMany(e => e.AudioBook)
                .UsingEntity<AudioBookGenre>();

            modelBuilder.Entity<AudioBook>()
                .HasMany(e => e.BookAudioFile)
                .WithMany(e => e.AudioBook)
                .UsingEntity<AudioBookAudioFile>();

            modelBuilder.Entity<AudioBook>()
                .HasMany(e => e.BookSelection)
                .WithMany(e => e.AudioBook)
                .UsingEntity<AudioBookSelection>();


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        public DbSet<AudioBook> AudioBook { get; set; }
        public DbSet<Narrator> Narrator { get; set; }
        public DbSet<BookSeries> BookSeries { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<BookAudioFile> BookAudioFile { get; set; }
        public DbSet<BookLanguage> BookLanguage { get; set; }
        public DbSet<BookSelection> BookSelection { get; set; }
        public DbSet<AudioBookAuthor> AudioBookAuthor { get; set; }
        public DbSet<AudioBookGenre> AudioBookGenre { get; set; }
        public DbSet<AudioBookAudioFile> AudioBookAudioFile { get; set; }
        public DbSet<AudioBookSelection> AudioBookSelection { get; set; }

    }
}
