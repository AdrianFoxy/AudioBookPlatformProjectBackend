using Re_ABP_Backend.Data.Entities.Picture;
using Re_ABP_Backend.Data.Interfraces;

namespace Re_ABP_Backend.Data.Services
{
    public class PictureService : IPictureService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PictureService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Picture> SaveToDiskAsync(IFormFile file, PictureType pictureType)
        {
            var picture = new Picture();
            if (file.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var webRootPath = _webHostEnvironment.WebRootPath;
                var filePath = Path.Combine(webRootPath, "img", pictureType.ToString(), fileName);

                await using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);

                picture.FileName = fileName;
                picture.PictureUrl = $"/img/{pictureType.ToString().ToLower()}/{fileName}";
                return picture;
            }
            return null;
        }

        public void DeleteFromDisk(string pictureName, PictureType pictureType)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(webRootPath, "img", pictureType.ToString(), pictureName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }


    }

}
