using Re_ABP_Backend.Data.Entities.Identity;
using Re_ABP_Backend.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Re_ABP_Backend.Data.Dtos.ReviewDtos
{
    public class ReviewCreateDto
    {
        [Required]
        public string ReviewText { get; set; }

        [Required]
        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public int Rating { get; set; }

        [Required]
        public int AudioBookId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
