using Re_ABP_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Re_ABP_Backend.Data.DB.Config
{
    public class BookSeriesConfiguration : IEntityTypeConfiguration<BookSeries>
    {
        public void Configure(EntityTypeBuilder<BookSeries> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
        }
    }
}
