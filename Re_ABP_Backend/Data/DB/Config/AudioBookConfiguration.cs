using Re_ABP_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Re_ABP_Backend.Data.DB.Config
{
    public class AudioBookConfiguration : IEntityTypeConfiguration<AudioBook>
    {
        public void Configure(EntityTypeBuilder<AudioBook> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            builder.Property(p => p.Description).IsRequired().HasColumnType("text");
            builder.Property(p => p.PictureUrl).IsRequired().HasColumnType("text");
            builder.Property(p => p.Rating).IsRequired().HasColumnType("float").HasDefaultValue(0);
            builder.Property(p => p.BookDuration)
                   .IsRequired()
                   .HasColumnType("int")
                   .HasDefaultValue(0);
            builder.Property(p => p.ViewCount)
                   .IsRequired()
                   .HasColumnType("int")
                   .HasDefaultValue(0);

            builder.HasIndex(p => p.Name).IsUnique();

            builder.HasOne(p => p.BookLanguage).WithMany()
                .HasForeignKey(p => p.BookLanguageId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Narrator).WithMany()
                .HasForeignKey(p => p.NarratorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.BookSeries).WithMany()
                .HasForeignKey(p => p.BookSeriesId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(g => g.AudioBookGenre)
                .WithOne(ag => ag.AudioBook)
                .HasForeignKey(ag => ag.AudioBookId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(g => g.AudioBookAuthor)
               .WithOne(ag => ag.AudioBook)
               .HasForeignKey(ag => ag.AudioBookId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
