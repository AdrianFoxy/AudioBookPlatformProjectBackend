using Microsoft.EntityFrameworkCore;
using Re_ABP_Backend.Data.DB;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Interfraces;

namespace Re_ABP_Backend.Data.Repositories
{
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly AppDBContext _context;
        public RecommendationRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<AudioBook>> GetRecommendationsByPopularity()
        {
            var popularBooks = await _context.AudioBook
                .Include(x => x.Author)
                .OrderByDescending(book => book.ViewCount)
                .Take(10)
                .ToListAsync();

            return popularBooks;
        }

        public async Task<IReadOnlyList<AudioBook>> GetRecommendationsByRating()
        {
            var topRatedBooks = await _context.AudioBook
                .OrderByDescending(book => book.Rating)
                .Include(x => x.Author)
                .Take(10)
                .ToListAsync();

            return topRatedBooks;
        }
    }
}
