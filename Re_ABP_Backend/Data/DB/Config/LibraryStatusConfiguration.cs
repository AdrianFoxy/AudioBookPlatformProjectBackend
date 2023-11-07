using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.DB.Config
{
    public class LibraryStatusConfiguration : IEntityTypeConfiguration<LibraryStatus>
    {
        public void Configure(EntityTypeBuilder<LibraryStatus> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            builder.Property(p => p.EnName).IsRequired().HasMaxLength(256);
        }
    }
}
