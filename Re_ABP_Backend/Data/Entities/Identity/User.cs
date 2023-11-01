using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Re_ABP_Backend.Data.Entities.Identity
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string About { get; set; }
        public bool SocialAuth { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime UpdatedAt { get; set; }
        public string Token { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

    }
}
