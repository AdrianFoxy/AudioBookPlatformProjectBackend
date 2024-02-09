using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.AudioBooksDtos.AudioFiles;
using Re_ABP_Backend.Data.Helpers.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.AudioBooksDtos
{
    public class AddAudioBookDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int BookLanguageId { get; set; }
        public int NarratorId { get; set; }
        public int BookSeriesId { get; set; }
        public int OrderInSeries { get; set; }
        public List<int> AuthorsIds { get; set; }   
        public List<int> GenresIds { get; set; }
        public string AudioFiles { get; set; }

        [NotMapped]
        [MaxFileSize(2 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".jpg", ".png", ".jpeg" })]
        public IFormFile? Picture { get; set; }

    }
}
