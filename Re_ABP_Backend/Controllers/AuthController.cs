﻿using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Re_ABP_Backend.Data.Dtos.AuthDtos;
using Re_ABP_Backend.Data.Dtos.UserDtos;
using Re_ABP_Backend.Data.Entities.Identity;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Errors;
using Re_ABP_Backend.Resources;
using System.Security.Claims;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _applicationSettings;
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;

        public AuthController(IUserService userService, 
                             IMapper mapper, 
                             IOptions<AppSettings> applicationSettings,
                             IStringLocalizer<SharedResource> sharedResourceLocalizer)
        {

            _userService = userService;
            _mapper = mapper;
            _applicationSettings = applicationSettings.Value;
            _sharedResourceLocalizer = sharedResourceLocalizer;

        }

        [HttpGet("get-current-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdentifier = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdentifier, out int userId))
            {
                var user = await _userService.GetUserById(userId);
                return Ok(_mapper.Map<User, UserDto>(user));
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userService.CheckEmailExistsAsync(email);
        }

        [HttpGet("usernameexists")]
        public async Task<ActionResult<bool>> CheckUserNameExistsAsync([FromQuery] string username)
        {
            return await _userService.CheckUserNameExistsAsync(username);
        }

        [HttpGet("refreshToken")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["X-Refresh-Token"];
            var user = await _userService.GetUserByRefreshToken(refreshToken);

            if(user == null || user.TokenExpires < DateTime.Now)
            {
                return Unauthorized(new ApiResponse(401));
            }
            _userService.CreateToken(user);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userService.GetUserByUserName(model.UserName);
            if (user == null) return Unauthorized(new ApiResponse(401));
            if (model.Password == string.Empty) return Unauthorized(new ApiResponse(401));

            var match =  _userService.CheckPassword(model.Password, user);
            if (!match)
                return Unauthorized(new ApiResponse(401));

            _userService.CreateToken(user);
            return Ok(_mapper.Map<User, UserDto>(user));
        }

        [HttpPost("loginWithGoogle")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] string credential)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { this._applicationSettings.GoogleClientId }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(credential, settings);

            var user = await _userService.GetUserByEmail(payload.Email);

            if (user != null)
            {
                _userService.CreateToken(user);
                return Ok(_mapper.Map<User, UserDto>(user));
            }
            else
            {
                var newuser = new RegisterDto
                {
                    UserName = payload.Name,
                    Email = payload.Email,
                    DateOfBirth = new DateTime(1900, 1, 1),
                    Password = string.Empty,
                    ConfirmPassword = string.Empty
                };
                var response = await _userService.AddUserAsync(newuser);
                if (response == false) return BadRequest(new ApiResponse(400));
                var user_reg = await _userService.GetUserByEmail(payload.Email);

                _userService.CreateToken(user_reg);
                return Ok(_mapper.Map<User, UserDto>(user_reg));
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var userEmailExists = await _userService.CheckEmailExistsAsync(model.Email);
            if (userEmailExists)
            {
                var errorMessage = _sharedResourceLocalizer.GetString("UserEmailExists");
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { errorMessage.Value } });
            }

            var userName = await _userService.CheckUserNameExistsAsync(model.UserName);
            if (userName)
            {
                var errorMessage = _sharedResourceLocalizer.GetString("UserNameExists");
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { errorMessage.Value } });
            }

            if (model.Password != model.ConfirmPassword)
            {
                var errorMessage = _sharedResourceLocalizer.GetString("PasswordConfirmValidation");
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { errorMessage.Value } });
            }

            var response = await _userService.AddUserAsync(model);
            if(response == false) return BadRequest(new ApiResponse(400));

            var user = await _userService.GetUserByUserName(model.UserName);
            _userService.CreateToken(user);
            return Ok(_mapper.Map<User, UserDto>(user));
        }

        [HttpDelete("logout")]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("X-Acces-Token", new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(-1),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            });
            return Ok();
        }

        [HttpDelete("revokeToken")]
        public IActionResult RevokeToken(string username)
        {
            _userService.RevokeToken(username);
            return Ok();
        }


    }
}
