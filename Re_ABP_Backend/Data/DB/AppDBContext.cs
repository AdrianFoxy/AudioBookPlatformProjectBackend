using Re_ABP_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Re_ABP_Backend.Data.Entities.Base;
using Re_ABP_Backend.Data.Entities.Identity;

namespace Re_ABP_Backend.Data.DB
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                 .ToTable(tb => tb.HasTrigger("UpdateAudioBookRating"));
            modelBuilder.Entity<Genre>()
                 .ToTable(tb => tb.HasTrigger("trg_UpdateGenreUpdatedAt"));
            modelBuilder.Entity<Author>()
                 .ToTable(tb => tb.HasTrigger("trg_UpdateAuthorUpdatedAt"));
            modelBuilder.Entity<AudioBook>()
                 .ToTable(tb => tb.HasTrigger("trg_UpdateAudioBookUpdatedAt"));
            modelBuilder.Entity<BookAudioFile>()
                 .ToTable(tb => tb.HasTrigger("trg_UpdateBookAudioFileUpdatedAt"));
            modelBuilder.Entity<BookLanguage>()
                 .ToTable(tb => tb.HasTrigger("trg_UpdateBookLanguageUpdatedAt"));
            modelBuilder.Entity<BookSelection>()
                 .ToTable(tb => tb.HasTrigger("trg_UpdateBookSelectionUpdatedAt"));
            modelBuilder.Entity<BookSeries>()
                 .ToTable(tb => tb.HasTrigger("trg_UpdateBookSeriesUpdatedAt"));
            modelBuilder.Entity<LibraryStatus>()
                 .ToTable(tb => tb.HasTrigger("trg_UpdateLibraryStatusUpdatedAt"));
            modelBuilder.Entity<Narrator>()
                 .ToTable(tb => tb.HasTrigger("trg_UpdateNarratorUpdatedAt"));
            modelBuilder.Entity<Review>()
                 .ToTable(tb => tb.HasTrigger("trg_UpdateReviewUpdatedAt"));
            modelBuilder.Entity<User>()
                 .ToTable(tb => tb.HasTrigger("trg_UpdateUserUpdatedAt"));
            modelBuilder.Entity<Role>()
                 .ToTable(tb => tb.HasTrigger("trg_UpdateRoleUpdatedAt"));

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

            modelBuilder.Entity<UserLibrary>()
              .HasKey(bc => new { bc.AudioBookId, bc.UserId });
            modelBuilder.Entity<UserLibrary>()
                .HasOne(bc => bc.AudioBook)
                .WithMany(b => b.UserLibrary)
                .HasForeignKey(bc => bc.AudioBookId);
            modelBuilder.Entity<UserLibrary>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.UserLibrary)
                .HasForeignKey(bc => bc.UserId);


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
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<UserLibrary> UserLibrary { get; set; }
        public DbSet<LibraryStatus> LibraryStatus { get; set; }

    }
}
