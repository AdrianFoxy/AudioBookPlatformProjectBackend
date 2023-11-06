using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Re_ABP_Backend.Data.Dtos.FilteringDtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Errors;
using Serilog;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilteringController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FilteringController(IUnitOfWork unitOfWork,
                                   IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("filter-genres")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<GenreFilteringDto>> GetFilteringGenresAsync()
        {
            var result = await _unitOfWork.Repository<Genre>().GetListAllAsync();
            if(result == null)
            {
                Log.Error("Request to get genres for filtering is failed, there is no data");
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper
                   .Map<IReadOnlyList<Genre>, IReadOnlyList<GenreFilteringDto>>(result));
        }

        [HttpGet("filter-authors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<AuthorFilteringDto>> GetFilteringAuthorsAsync()
        {
            var result = await _unitOfWork.Repository<Author>().GetListAllAsync();
            if (result == null)
            {
                Log.Error("Request to get authors for filtering is failed, there is no data");
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper
                   .Map<IReadOnlyList<Author>, IReadOnlyList<AuthorFilteringDto>>(result));
        }

        [HttpGet("filter-bookLanguage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<BookLanguageFilteringDto>> GetFilteringBookLanguagesAsync()
        {
            var result = await _unitOfWork.Repository<BookLanguage>().GetListAllAsync();
            if (result == null)
            {
                Log.Error("Request to get book languages for filtering is failed, there is no data");
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper
                   .Map<IReadOnlyList<BookLanguage>, IReadOnlyList<BookLanguageFilteringDto>>(result));
        }

        [HttpGet("filter-bookSeries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<BookSeriesFilteringDto>> GetFilteringBookSeriesAsync()
        {
            var result = await _unitOfWork.Repository<BookSeries>().GetListAllAsync();
            if (result == null)
            {
                Log.Error("Request to get bookSeries for filtering is failed, there is no data");
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper
                   .Map<IReadOnlyList<BookSeries>, IReadOnlyList<BookSeriesFilteringDto>>(result));
        }

        [HttpGet("filter-narrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<NarratorFilteringDto>> GetFilteringNarratorsAsync()
        {
            var result = await _unitOfWork.Repository<Narrator>().GetListAllAsync();
            if (result == null)
            {
                Log.Error("Request to get narrators for filtering is failed, there is no data");
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper
                   .Map<IReadOnlyList<Narrator>, IReadOnlyList<NarratorFilteringDto>>(result));
        }
    }
}
