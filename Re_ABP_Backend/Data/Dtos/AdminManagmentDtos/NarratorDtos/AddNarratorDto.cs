using System.ComponentModel.DataAnnotations;

namespace Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.NarratorDtos
{
    public class AddNarratorDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Max lenght is 100.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(400, ErrorMessage = "Max lenght is 400.")]
        public string MediaUrl { get; set; }

    }
}
