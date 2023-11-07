using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Re_ABP_Backend.Data.Dtos;
using Re_ABP_Backend.Data.Dtos.UserDtos;
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
    public class UserLibraryController : ControllerBase
    {
        private readonly IUserLibraryRepository _userLibraryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserLibraryController(IUserLibraryRepository userLibraryRepository, IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _userLibraryRepository = userLibraryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ManagaLibraryStatus(UserLibraryDto userLibraryDto)
        {
            var userIdentifier = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            int? userId = int.TryParse(userIdentifier, out int parsedUserId) ? parsedUserId : null;
            if (userId.HasValue && userId == userLibraryDto.UserId)
            {
                var existsUser = await _userService.GetUserById(userLibraryDto.UserId);
                if (existsUser == null)
                    return NotFound(new ApiResponse(404, "User with this ID, does not exist"));

                var existsBook = await _unitOfWork.Repository<AudioBook>().GetByIdAsync(userLibraryDto.AudioBookId);
                if (existsBook == null)
                    return NotFound(new ApiResponse(404, "Audiobook with this ID, does not exist"));

                var result = await _userLibraryRepository.ManageBookInUserLibrary(userLibraryDto);
                if (result == false)
                    return BadRequest(new ApiResponse(400, "Problem managing review."));

                return Ok();
            } 
            return NotFound(new ApiResponse(401, "You cannot manage someone else's library"));
        }
    }
}
