using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Re_ABP_Backend.Data.Entities.Identity;

namespace Re_ABP_Backend.Data.DB.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Email).IsRequired().HasMaxLength(256);
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(256);
            builder.Property(p => p.About).IsRequired().HasMaxLength(256);
            builder.Property(p => p.SocialAuth).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.PasswordHash).IsRequired().HasMaxLength(256);
            builder.Property(p => p.PasswordSalt).IsRequired().HasMaxLength(256);
            builder.HasOne(p => p.Role).WithMany()
                        .HasForeignKey(p => p.RoleId)
                        .OnDelete(DeleteBehavior.Restrict);
            builder.Property(p => p.CreatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(p => p.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(p => p.Token).IsRequired().HasDefaultValue(string.Empty);
            builder.Property(p => p.TokenCreated).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(p => p.TokenExpires).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");

        }
    }
}
