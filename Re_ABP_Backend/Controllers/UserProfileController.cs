using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Re_ABP_Backend.Data.Dtos.UserDtos;
using Re_ABP_Backend.Data.Entities.Identity;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Errors;
using Serilog;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserProfileController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUserByEmail(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                Log.Error("Request to get user by id failed, user with id {id} does not exists.", id);
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<User, UserDto>(user));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> UpdateUser(UserDto userUpdate)
        {
            if (!(await _userService.EditUserAsync(userUpdate)))
                return BadRequest(new ApiResponse(400));
            var user = await _userService.GetUserById(userUpdate.Id);
            if (user == null)
            {
                Log.Error("Request to get user by id failed, user with id {userUpdate.Id} does not exists.", userUpdate.Id);
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<User, UserDto>(user));
        }

    }
}
