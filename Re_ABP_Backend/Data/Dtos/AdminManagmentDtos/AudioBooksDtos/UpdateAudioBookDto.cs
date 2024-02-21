using Re_ABP_Backend.Data.Helpers.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.AudioBooksDtos
{
    public class UpdateAudioBookDto
    {
        [Required]
        [MaxLength(256, ErrorMessage = "Max lenght is 256.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(800, ErrorMessage = "Max lenght is 800.")]
        public string Description { get; set; }
        [Required]
        public int BookLanguageId { get; set; }
        [Required]
        public int NarratorId { get; set; }
        [Required]
        public int BookSeriesId { get; set; }
        [Required]
        public int OrderInSeries { get; set; }
        [Required]
        public List<int> AuthorsIds { get; set; }
        [Required]
        public List<int> GenresIds { get; set; }
        [Required]
        public List<int>? AudioFilesToDelete { get; set; }
        [Required]
        public string AudioFiles { get; set; }

        [NotMapped]
        [MaxFileSize(2 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".jpg", ".png", ".jpeg" })]
        public IFormFile? Picture { get; set; }

    }
}
