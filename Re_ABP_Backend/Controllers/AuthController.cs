using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Re_ABP_Backend.Data.Dtos.AuthDtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Entities.Identity;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Errors;
using Serilog;
using System.Security.Claims;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IMapper mapper)
        {

            _userService = userService;
            _mapper = mapper;
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

        [HttpGet("get-current-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var username = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.GetUserByUserName(username);
            return Ok(_mapper.Map<User, UserDto>(user));
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

/*            return Unauthorized();
*/
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userService.GetUserByUserName(model.UserName);
            if (user == null) return Unauthorized(new ApiResponse(401));

            var match =  _userService.CheckPassword(model.Password, user);
            if (!match)
                return Unauthorized(new ApiResponse(401));

            _userService.CreateToken(user);
            return Ok(new { username = user.UserName, email = user.Email, dateOfBirth = user.DateOfBirth });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var userEmail = await _userService.CheckEmailExistsAsync(model.Email);
            if (userEmail)
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } });

            var userName = await _userService.CheckUserNameExistsAsync(model.UserName);
            if (userName)
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "UserName is in use" } });

            if (model.Password != model.ConfirmPassword)
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Password confirm is wrong" } });

            var response = await _userService.AddUserAsync(model);
            if(response == false) return BadRequest(new ApiResponse(400));

            var user = await _userService.GetUserByUserName(model.UserName);
            _userService.CreateToken(user);
            return Ok(new { username = user.UserName, email = user.Email, dateOfBirth = user.DateOfBirth });
        }

        [HttpDelete("logout")]
        public async Task<IActionResult> LogOut()
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
        public async Task<IActionResult> RevokeToken()
        {
            var refreshToken = Request.Cookies["X-Refresh-Token"];
            var user = await _userService.GetUserByRefreshToken(refreshToken);

            _userService.RevokeToken(user.UserName);
            return Ok();
        }

        
    }
}
