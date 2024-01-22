using Re_ABP_Backend.Data.Dtos;

namespace Re_ABP_Backend.Data.Interfraces
{
    public interface IUserLibraryService
    {
        Task<bool> ManageBookInUserLibrary(UserLibraryDto userLibraryDto);
        Task<int> GetBookmarkCountForAudioBookAsync(int audioBookId);
    }
}
