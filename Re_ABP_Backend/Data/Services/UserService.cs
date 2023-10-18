﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Re_ABP_Backend.Data.DB;
using Re_ABP_Backend.Data.Dtos.AuthDtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Entities.Identity;
using Re_ABP_Backend.Data.Interfraces;
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

        public UserService(AppDBContext context, IOptions<AppSettings> applicationSettings)
        {
            _context = context;
            _applicationSettings = applicationSettings.Value;
        }

        public string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._applicationSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.UserName), new Claim(ClaimTypes.Email, user.Email), new Claim(ClaimTypes.Role, user.Role.Name) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encrypterToken = tokenHandler.WriteToken(token);
            return encrypterToken;
        }
        public async Task<User?> GetUserByUserName(string username)
        {
            return await _context.User
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserName == username);
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

        public async Task<bool> AddUserAsync(RegisterDto model)
        {
            try
            {
                CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var user = new User
                {
                    FullName = model.UserName,
                    Email = model.Email,
                    UserName = model.UserName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    RoleId = 2
                };

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