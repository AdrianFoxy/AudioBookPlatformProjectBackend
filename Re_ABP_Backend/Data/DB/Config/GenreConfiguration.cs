using Re_ABP_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Re_ABP_Backend.Data.DB.Config
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            builder.Property(p => p.EnName).IsRequired().HasMaxLength(256);

            builder.HasIndex(p => p.Name).IsUnique();
            builder.HasIndex(p => p.EnName).IsUnique();

            builder.HasMany(g => g.AudioBookGenre)
                   .WithOne(ag => ag.Genre)
                   .HasForeignKey(ag => ag.GenreId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
