using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Re_ABP_Backend.Data.DB;
using Re_ABP_Backend.Data.Dtos.AuthDtos;
using Re_ABP_Backend.Data.Dtos.UserDtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Entities.Identity;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Errors;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Re_ABP_Backend.Data.Services
{
    public class UserService : IUserService
    {
        private AppDBContext _context;
        private readonly AppSettings _applicationSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(AppDBContext context, IOptions<AppSettings> applicationSettings, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _applicationSettings = applicationSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._applicationSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), new Claim(ClaimTypes.Name, user.UserName), new Claim(ClaimTypes.Email, user.Email), new Claim(ClaimTypes.Role, user.Role.Name) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encrypterToken = tokenHandler.WriteToken(token);

            _httpContextAccessor.HttpContext.Response.Cookies.Append("X-Acces-Token", encrypterToken,
             new CookieOptions
             {
                 Expires = DateTime.Now.AddHours(6),
                 HttpOnly = true,
                 Secure = true,
                 IsEssential = true,
                 SameSite = SameSiteMode.None
             });

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, user);

            return encrypterToken;
        }

        public void SetRefreshToken(RefreshToken refreshToken, User user)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append("X-Refresh-Token", refreshToken.Token,
                new CookieOptions
                {
                    Expires = refreshToken.Expires,
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None
                });

            var existingUser = _context.User.FirstOrDefault(u => u.UserName == user.UserName);

            if (existingUser !=null)
            {
                existingUser.Token = refreshToken.Token;
                existingUser.TokenCreated = refreshToken.Created;
                existingUser.TokenExpires = refreshToken.Expires;
                _context.SaveChanges();
            }
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        public void RevokeToken(string username)
        {
            var user = _context.User.FirstOrDefault(x => x.UserName == username);
            if (user != null)
            {
                user.Token = string.Empty;
                _context.SaveChanges();
            }

        }

        public async Task<User> GetUserByRefreshToken(string refreshToken)
        {
            return await _context.User.Where(x => x.Token == refreshToken).Include(u => u.Role).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByUserName(string username)
        {
            return await _context.User
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.User
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User?> GetUserById(int id)
        {
            return await _context.User
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public bool CheckPassword(string password, User user)
        {
            bool result;

            using (HMACSHA512? hmac = new HMACSHA512(user.PasswordSalt))
            {
                var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                result = compute.SequenceEqual(user.PasswordHash);
            }

            return result;
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            var userEmail = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
            return userEmail != null;
        }
        public async Task<bool> CheckUserNameExistsAsync(string username)
        {
            var userName = await _context.User.FirstOrDefaultAsync(u => u.UserName == username);
            return userName != null;
        }
        public async Task<bool> NewReviewAllowed(int audioBookId, int userId)
        {
            var reviewExists = await _context.Review.FirstOrDefaultAsync(r => r.AudioBookId == audioBookId && r.UserId == userId);
            return reviewExists != null;
        }

        public async Task<bool> AddUserAsync(RegisterDto model)
        {
            try
            {
                CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var user = new User
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    DateOfBirth = model.DateOfBirth,
                    RoleId = 2,
                    About = string.Empty
                };
                if (model.Password.IsNullOrEmpty()) user.SocialAuth = true;

                await _context.User.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Log.Error("AddUserAsync METHOD: Cant add new User to DB. {ex}", ex);
                return false;
            }
        }

        public async Task<bool> EditUserAsync(UserDto user)
        {
            try
            {
                var dbUser = await _context.User.FindAsync(user.Id);
                if(dbUser == null)
                {
                    Log.Error("Request to get user by id failed, user with id {user.Id} does not exists.", user.Id);
                    return false;
                }

                if(dbUser.Email != user.Email && dbUser.SocialAuth == true) 
                {
                    Log.Error("Cannot update user email, what auth wia social media (Google). {user.Email}", user.Email);
                    return false;
                }

                dbUser.UserName = user.UserName;
                dbUser.Email = user.Email;
                dbUser.DateOfBirth = user.DateOfBirth;
                dbUser.About = user.About;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Log.Error("EditUserAsync METHOD: Can't update user information. {ex}", ex);
                return false;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
