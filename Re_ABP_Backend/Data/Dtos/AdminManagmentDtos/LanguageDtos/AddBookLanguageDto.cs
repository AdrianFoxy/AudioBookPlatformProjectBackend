using System.ComponentModel.DataAnnotations;

namespace Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.LanguageDtos
{
    public class AddBookLanguageDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Max lenght is 100.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Max lenght is 100.")]
        public string EnName { get; set; }
    }
}
