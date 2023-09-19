using ABP_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABP_Backend.Data.DB.Config
{
    public class BookAudioFileConfiguration : IEntityTypeConfiguration<BookAudioFile>
    {
        public void Configure(EntityTypeBuilder<BookAudioFile> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            builder.Property(p => p.AudioFileUrl).IsRequired().HasColumnType("text");
            builder.Property(p => p.Duration).IsRequired().HasColumnType("time").HasDefaultValueSql("'00:00:00'");
        }
    }
}
