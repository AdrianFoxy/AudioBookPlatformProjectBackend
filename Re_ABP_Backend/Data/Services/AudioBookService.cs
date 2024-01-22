using Re_ABP_Backend.Data.DB;
using Re_ABP_Backend.Data.Interfraces;
using Serilog;

namespace Re_ABP_Backend.Data.Services
{
    public class AudioBookService : IAudioBookService
    {
        private readonly AppDBContext _context;
        public AudioBookService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> IncreaseViewCountAsync(int audioBookId)
        {
            var audioBook = await _context.AudioBook.FindAsync(audioBookId);

            if (audioBook == null)
            {
                Log.Error("IncreaseViewCountAsync METHOD: Request to get audiobook by id failed, book with id {audioBookId} does not exists.", audioBookId);
                return false;
            }

            audioBook.ViewCount++;
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
