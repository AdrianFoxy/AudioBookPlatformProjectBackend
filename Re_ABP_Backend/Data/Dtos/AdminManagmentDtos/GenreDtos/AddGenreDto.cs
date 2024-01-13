using System.ComponentModel.DataAnnotations;

namespace Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.GenreDtos
{
    public class AddGenreDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Max lenght is 50.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max lenght is 50.")]
        public string EnName { get; set; }

    }
}
