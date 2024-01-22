using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Re_ABP_Backend.Data.Dtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Helpers;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Data.Specification.Params;
using Re_ABP_Backend.Data.Specification.SpecClasses.AudioBookSpec;
using Re_ABP_Backend.Data.Specification.SpecClasses.AudioBookSpec.Count;
using Re_ABP_Backend.Errors;
using System.Security.Claims;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLibraryController : ControllerBase
    {
        private readonly IUserLibraryService _userLibraryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserLibraryController(IUserLibraryService userLibraryRepository, IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _userLibraryRepository = userLibraryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<AudioBookInLibraryDto>>> GetAudioBooks(
            [FromQuery] UserLibraryParams userLibraryParams)
        {
            var spec = new UserLibrarySpecification(userLibraryParams);
            var countSpec = new UserLibraryForCountSpecification(userLibraryParams);

            var totalItems = await _unitOfWork.Repository<AudioBook>().CountAsync(countSpec);

            var abooks = await _unitOfWork.Repository<AudioBook>().GetListWithSpecAsync(spec);

            var data = _mapper
                .Map<IReadOnlyList<AudioBook>, IReadOnlyList<AudioBookInLibraryDto>>(abooks);

            return Ok(new Pagination<AudioBookInLibraryDto>(userLibraryParams.PageIndex,
                userLibraryParams.PageSize, totalItems, data));
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
            return Unauthorized(new ApiResponse(401, "You cannot manage someone else's library"));
        }
    }
}
