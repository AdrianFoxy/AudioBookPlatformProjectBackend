using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Interfraces
{
    public interface IAudioBookRepository
    {
        Task<AudioBook> GetAudioBookByIdAsync(int id);
        Task<IReadOnlyList<AudioBook>> GetAudioBooksAsync();
    }
}
