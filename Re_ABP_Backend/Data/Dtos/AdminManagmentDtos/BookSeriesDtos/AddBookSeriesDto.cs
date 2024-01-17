using System.ComponentModel.DataAnnotations;

namespace Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.BookSeriesDtos
{
    public class AddBookSeriesDto
    {
        [Required]
        [MaxLength(254, ErrorMessage = "Max lenght is 254.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(254, ErrorMessage = "Max lenght is 254.")]
        public string EnName { get; set; }

    }
}
