using Microsoft.EntityFrameworkCore;
using Re_ABP_Backend.Data.DB;
using Re_ABP_Backend.Data.Dtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Interfraces;

namespace Re_ABP_Backend.Data.Services
{
    public class UserLibraryService : IUserLibraryService
    {
        private readonly AppDBContext _context;
        public UserLibraryService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> ManageBookInUserLibrary(UserLibraryDto userLibraryDto)
        {
            var userLibraryEntry = await _context.UserLibrary
                .FirstOrDefaultAsync(ul => ul.UserId == userLibraryDto.UserId && ul.AudioBookId == userLibraryDto.AudioBookId);

            if (userLibraryEntry != null)
            {
                if (userLibraryDto.LibraryStatusId == 0)
                    _context.UserLibrary.Remove(userLibraryEntry);
                userLibraryEntry.LibraryStatusId = userLibraryDto.LibraryStatusId;
            }
            else
            {
                userLibraryEntry = new UserLibrary
                {
                    UserId = userLibraryDto.UserId,
                    AudioBookId = userLibraryDto.AudioBookId,
                    LibraryStatusId = userLibraryDto.LibraryStatusId,
                };
                _context.UserLibrary.Add(userLibraryEntry);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetBookmarkCountForAudioBookAsync(int audioBookId)
        {
            return await _context.UserLibrary
                .CountAsync(ul => ul.AudioBookId == audioBookId);
        }

    }
}
