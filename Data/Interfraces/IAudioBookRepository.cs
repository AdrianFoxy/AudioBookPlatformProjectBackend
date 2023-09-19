using ABP_Backend.Data.Entities;

namespace ABP_Backend.Data.Interfraces
{
    public interface IAudioBookRepository
    {
        Task<AudioBook> GetAudioBookByIdAsync(int id);
        Task<IReadOnlyList<AudioBook>> GetAudioBooksAsync();
    }
}
