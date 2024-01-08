using Re_ABP_Backend.Data.Dtos.AuthDtos;
using Re_ABP_Backend.Data.Dtos.UserDtos;
using Re_ABP_Backend.Data.Entities.Identity;

namespace Re_ABP_Backend.Data.Interfraces
{
    public interface IUserService
    {
        Task<User?> GetUserByUserName(string username);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(int id);
        Task<User> GetUserByRefreshToken(string refreshToken);
        void RevokeToken(string username);
        bool CheckPassword(string password, User user);
        string CreateToken(User user);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<bool> CheckUserNameExistsAsync(string username);
        Task<bool> NewReviewAllowed(int audioBookId, int userId);
        Task<bool> AddUserAsync(RegisterDto model);
        Task<bool> EditUserAsync(UserDto user);
        Task<int> GetLibraryStatusIdAsync(int userId, int audioBookId);
        Task<int> UserCountAsync();

    }
}
