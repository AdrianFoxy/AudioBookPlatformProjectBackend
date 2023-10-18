using Re_ABP_Backend.Data.DB;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Interfraces;
using Microsoft.EntityFrameworkCore;
using Re_ABP_Backend.Errors;
using Serilog;

namespace Re_ABP_Backend.Data.Repositories
{
    public class AudioBookRepository : IAudioBookRepository
    {
        private readonly AppDBContext _context;
        public AudioBookRepository(AppDBContext context) 
        {
            _context = context;
        }
        public async Task<AudioBook?> GetAudioBookByIdAsync(int id)
        {
            return await _context.AudioBook.FindAsync(id);
        }

        public async Task<IReadOnlyList<AudioBook>> GetAudioBooksAsync()
        {
            return await _context.AudioBook.ToListAsync();
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
