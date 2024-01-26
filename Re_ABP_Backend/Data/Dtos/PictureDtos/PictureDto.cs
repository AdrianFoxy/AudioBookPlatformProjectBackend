using Re_ABP_Backend.Data.Helpers.Validation;

namespace Re_ABP_Backend.Data.Dtos.PictureDtos
{
    public class PictureDto
    {
        [MaxFileSize(2 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".jpg", ".png", ".jpeg" })]
        public IFormFile Picture { get; set; }
    }
}
