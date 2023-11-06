using Re_ABP_Backend.Data.Entities.Identity;
using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Dtos.ReviewDtos
{
    public class ReviewCreateDto
    {
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public int AudioBookId { get; set; }
        public int UserId { get; set; }
    }
}
