using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Re_ABP_Backend.Data.Entities.Identity;

namespace Re_ABP_Backend.Data.DB.Config
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            builder.Property(p => p.CreatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(p => p.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
