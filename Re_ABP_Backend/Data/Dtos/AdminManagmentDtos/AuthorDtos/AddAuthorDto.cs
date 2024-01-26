using Newtonsoft.Json;
using Re_ABP_Backend.Data.Helpers.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.AuthorDtos
{
    public class AddAuthorDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string EnName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string EnDescription { get; set; }

        [NotMapped]
        [MaxFileSize(2 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".jpg", ".png", ".jpeg" })]
        public IFormFile? Picture { get; set; }

    }
}
