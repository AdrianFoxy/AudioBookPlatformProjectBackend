using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Interfraces
{
    public interface IAudioBookService
    {
        Task<bool> IncreaseViewCountAsync(int audioBookId);
    }
}
