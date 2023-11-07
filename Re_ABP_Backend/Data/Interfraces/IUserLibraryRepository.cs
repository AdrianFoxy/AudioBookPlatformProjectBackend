using Re_ABP_Backend.Data.Dtos;

namespace Re_ABP_Backend.Data.Interfraces
{
    public interface IUserLibraryRepository
    {
        Task<bool> ManageBookInUserLibrary(UserLibraryDto userLibraryDto);
    }
}
