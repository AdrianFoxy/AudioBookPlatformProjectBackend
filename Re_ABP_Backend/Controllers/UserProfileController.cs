using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Re_ABP_Backend.Data.Dtos.UserDtos;
using Re_ABP_Backend.Data.Entities.Identity;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Errors;
using Re_ABP_Backend.Resources;
using Serilog;
using System.Security.Claims;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;

        public UserProfileController(IUserService userService, IMapper mapper,
                                     IStringLocalizer<SharedResource> sharedResourceLocalizer)
        {
            _userService = userService;
            _mapper = mapper;
            _sharedResourceLocalizer = sharedResourceLocalizer;
        }

        [HttpGet("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUserByUserName(string username)
        {
            var user = await _userService.GetUserByUserName(username);
            if (user == null)
            {
                Log.Error("Request to get user by username failed, user with username {username} does not exists.", username);
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<User, UserDto>(user));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<ActionResult<UserDto>> UpdateUser(UserDto userUpdate)
        {
            var user = await _userService.GetUserById(userUpdate.Id);
            var userIdentifier = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userIdentifier != userUpdate.Id.ToString())
                return BadRequest(new ApiResponse(403));


            if (user.Email != userUpdate.Email)
            {
                var userEmail = await _userService.CheckEmailExistsAsync(userUpdate.Email);
                if (userEmail)
                {
                    var errorMessage = _sharedResourceLocalizer.GetString("UserEmailExists");
                    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { errorMessage.Value } });
                }
            }

            if (user.UserName != userUpdate.UserName)
            {
                var userName = await _userService.CheckUserNameExistsAsync(userUpdate.UserName);
                if (userName)
                {
                    var errorMessage = _sharedResourceLocalizer.GetString("UserNameExists");
                    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { errorMessage.Value } });
                }
            }

            if (!(await _userService.EditUserAsync(userUpdate)))
                return BadRequest(new ApiResponse(400));

            user = await _userService.GetUserById(userUpdate.Id);

            if (user == null)
            {
                Log.Error("Request to get user by id failed, user with id {userUpdate.Id} does not exists.", userUpdate.Id);
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<User, UserDto>(user));
        }

    }
}
