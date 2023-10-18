using Re_ABP_Backend.Data.Dtos.AuthDtos;
using Re_ABP_Backend.Data.Entities.Identity;

namespace Re_ABP_Backend.Data.Interfraces
{
    public interface IUserService
    {
        Task<User?> GetUserByUserName(string username);
        bool CheckPassword(string password, User user);
        string CreateToken(User user);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<bool> CheckUserNameExistsAsync(string username);
        Task<bool> AddUserAsync(RegisterDto model);
    }
}
