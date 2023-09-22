using ABP_Backend.Data.DB;
using ABP_Backend.Data.Entities;
using ABP_Backend.Data.Interfraces;
using Microsoft.EntityFrameworkCore;

namespace ABP_Backend.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDBContext _context;
        public GenericRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}
