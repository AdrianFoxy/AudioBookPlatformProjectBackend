using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Re_ABP_Backend.Data.Dtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Errors;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService) {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("auth-test")]
        public string GetTest()
        {
            return "Hi auth user!";
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("auth-test-admin")]
        public string GetTestTest()
        {
            return "Hi admin!";
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userService.GetUserByUserName(model.UserName);
            if (user == null) return Unauthorized(new ApiResponse(401));

            var match =  _userService.CheckPassword(model.Password, user);
            if (!match)
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Passwords do not match" } });

            return Ok(new { token =  _userService.CreateToken(user), username = user.UserName });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var userEmail = await _userService.CheckEmailExistsAsync(model.Email);
            if (userEmail)
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } });
            var userName = await _userService.CheckUserNameExistsAsync(model.UserName);
            if (userName)
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "UserName is in use" } });

            var response = await _userService.AddUserAsync(model);
            if(response == false) return BadRequest(new ApiResponse(400));

            var user = await _userService.GetUserByUserName(model.UserName);
            return Ok(new { token = _userService.CreateToken(user), username = user.UserName });
        }

    }
}
