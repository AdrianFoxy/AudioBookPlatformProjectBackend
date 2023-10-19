using System.ComponentModel.DataAnnotations;

namespace Re_ABP_Backend.Data.Dtos.AuthDtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }
        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
            ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 number, 1 non alphanumeric and at least 6 characters")]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

    }
}
