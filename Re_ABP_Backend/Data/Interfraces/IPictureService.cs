using Re_ABP_Backend.Data.Entities.Picture;

namespace Re_ABP_Backend.Data.Interfraces
{
    public interface IPictureService
    {
        Task<Picture> SaveToDiskAsync(IFormFile photo, PictureType pictureType);
        void DeleteFromDisk(string pictureName, PictureType pictureType);
    }
}
