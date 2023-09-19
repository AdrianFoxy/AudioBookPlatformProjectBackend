using ABP_Backend.Data.DB;
using ABP_Backend.Data.Entities;
using ABP_Backend.Data.Interfraces;
using Microsoft.EntityFrameworkCore;

namespace ABP_Backend.Data.Repositories
{
    public class AudioBookRepository : IAudioBookRepository
    {
        private readonly AppDBContext _context;
        public AudioBookRepository(AppDBContext context) 
        {
            _context = context;
        }
        public async Task<AudioBook> GetAudioBookByIdAsync(int id)
        {
            return await _context.AudioBook.FindAsync(id);
        }

        public async Task<IReadOnlyList<AudioBook>> GetAudioBooksAsync()
        {
            return await _context.AudioBook.ToListAsync();
        }
    }
}
