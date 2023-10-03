using Re_ABP_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Re_ABP_Backend.Data.DB.Config
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            builder.Property(p => p.EnName).IsRequired().HasMaxLength(256);
            builder.Property(p => p.ImageUrl).IsRequired().HasColumnType("text");
            builder.Property(p => p.Description).IsRequired().HasColumnType("text");
            builder.Property(p => p.EnDescription).IsRequired().HasColumnType("text");
        }
    }
}