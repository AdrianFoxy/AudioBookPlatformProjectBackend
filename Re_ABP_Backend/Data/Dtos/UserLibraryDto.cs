using Re_ABP_Backend.Data.Entities.Identity;
using Re_ABP_Backend.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Re_ABP_Backend.Data.Dtos
{
    public class UserLibraryDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int AudioBookId { get; set; }

        [Required]
        [Range(0, 3, ErrorMessage = "Rating must be between 0 and 3.")]
        public int LibraryStatusId { get; set; }
    }
}
