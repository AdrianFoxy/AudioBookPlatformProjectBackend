using System.ComponentModel.DataAnnotations;

namespace Re_ABP_Backend.Data.Dtos.UserDtos
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [MaxLength(256)]
        public string About { get; set; }
        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }
        public string Role { get; set; }
        public bool SocialAuth { get; set; }

    }
}
