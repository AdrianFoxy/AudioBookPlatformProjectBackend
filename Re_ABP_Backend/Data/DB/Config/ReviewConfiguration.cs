using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.DB.Config
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(p => p.ReviewText).IsRequired().HasColumnType("text");
            builder.Property(p => p.Rating).IsRequired().HasColumnType("int");
        }
    }
}
