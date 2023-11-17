using Re_ABP_Backend.Data.Dtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Data.Specification.SpecClasses;
using Re_ABP_Backend.Errors;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Re_ABP_Backend.Data.Helpers;
using Re_ABP_Backend.Data.Specification.SpecClasses.AudioBooks;
using Re_ABP_Backend.Data.Specification.SpecClasses.AudioBookSpec.Count;
using System.Security.Claims;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioBookController : ControllerBase
    {
        private readonly IAudioBookRepository _audioBookRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUserLibraryRepository _userLibraryRepository;
        public AudioBookController(IUnitOfWork unitOfWork,
                                   IAudioBookRepository audioBookRepository,
                                   IMapper mapper,
                                   IUserService userService,
                                   IUserLibraryRepository userLibraryRepository)
        {
            _audioBookRepository = audioBookRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userService = userService;
            _userLibraryRepository = userLibraryRepository; 
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<AudioBookInLibraryDto>>> GetBooksAsync(
            [FromQuery] ABSpecParams abParams)
        {
            var spec = new LibraryAudioBookSpecification(abParams);
            var countSpec = new LibraryAudioBookForCountSpecification(abParams);
            

            var totalItems = await _unitOfWork.Repository<AudioBook>().CountAsync(countSpec);

            var abooks = await _unitOfWork.Repository<AudioBook>().GetListWithSpecAsync(spec);

            var data = _mapper
                .Map<IReadOnlyList<AudioBook>, IReadOnlyList<AudioBookInLibraryDto>>(abooks);

            return Ok(new Pagination<AudioBookInLibraryDto>(abParams.PageIndex,
                abParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SingleAudioBookDto>> GetBookAsync(int id)
        {
            var spec = new LibraryAudioBookSpecification(id);
            var aidiobook = await _unitOfWork.Repository<AudioBook>().GetEntityWithSpec(spec);

            if (aidiobook == null) 
            {
                Log.Error("Request to get audiobook by id failed, book with id {Id} does not exists.", id);
                return NotFound(new ApiResponse(404));
            }

            var data = _mapper
                  .Map<AudioBook, SingleAudioBookDto>(aidiobook);

            data.BookMarksCount = await _userLibraryRepository.GetBookmarkCountForAudioBookAsync(data.Id);

            var userIdentifier = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdentifier != null && int.TryParse(userIdentifier, out int userId))
            {
                data.LibraryStatusId = await _userService.GetLibraryStatusIdAsync(userId, data.Id);
            }
            return Ok(data);
        }

        [HttpPut("increment-viewcount/{id}")]
        public async Task<ActionResult> IncrementViewCount(int id)
        {
            var response = await _audioBookRepository.IncreaseViewCountAsync(id);
            return Ok(response);
        }
    }
}
