using Microsoft.EntityFrameworkCore;
using Re_ABP_Backend.Data.DB;
using Re_ABP_Backend.Data.Interfraces;

namespace Re_ABP_Backend.Data.Repositories
{
    public class DashboardRepository : IDashboard
    {
        private readonly AppDBContext _context;
        public DashboardRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<int[]> GetUserCountByMothAsync()
        {
            var users = await _context.User
                .Where(user => user.CreatedAt.Year == DateTime.Now.Year)
                .ToListAsync();

            int[] usersByMonth = new int[12];

            foreach (var user in users)
            {
                int monthIndex = user.CreatedAt.Month - 1;
                usersByMonth[monthIndex]++;
            }

            return usersByMonth;
        }
    }
}
