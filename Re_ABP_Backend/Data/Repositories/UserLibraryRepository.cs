using Microsoft.EntityFrameworkCore;
using Re_ABP_Backend.Data.DB;
using Re_ABP_Backend.Data.Dtos;
using Re_ABP_Backend.Data.Entities.Identity;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Interfraces;

namespace Re_ABP_Backend.Data.Repositories
{
    public class UserLibraryRepository : IUserLibraryRepository
    {
        private readonly AppDBContext _context;
        public UserLibraryRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> ManageBookInUserLibrary(UserLibraryDto userLibraryDto)
        {
            var userLibraryEntry = await _context.UserLibrary
                .FirstOrDefaultAsync(ul => ul.UserId == userLibraryDto.UserId && ul.AudioBookId == userLibraryDto.AudioBookId);

            if(userLibraryEntry != null) 
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
    }
}
