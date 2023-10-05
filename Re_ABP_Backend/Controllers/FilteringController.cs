using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Re_ABP_Backend.Data.Dtos;
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
        private readonly IGenericRepository<Genre> _genreRepo;
        private readonly IGenericRepository<Author> _authorRepo;
        private readonly IGenericRepository<BookLanguage> _bookLanguageRepo;
        private readonly IGenericRepository<BookSeries> _bookSeriesRepo;
        private readonly IGenericRepository<Narrator> _narratorRepo;
        private readonly IMapper _mapper;

        public FilteringController(IGenericRepository<Genre> genreRepo,
                           IGenericRepository<Author> authorRepo,
                           IGenericRepository<BookLanguage> bookLanguageRepo,
                           IGenericRepository<BookSeries> bookSeriesRepo,
                           IGenericRepository<Narrator> narratorRepo,
                           IMapper mapper)
        {
            _genreRepo = genreRepo;
            _authorRepo = authorRepo;
            _bookLanguageRepo = bookLanguageRepo;
            _bookSeriesRepo = bookSeriesRepo;
            _narratorRepo = narratorRepo;
            _mapper = mapper;
        }

        [HttpGet("filter-genres")]
        public async Task<ActionResult<GenreFilteringDto>> GetFilteringGenresAsync()
        {
            var result = await _genreRepo.GetListAllAsync();
            if(result == null)
            {
                Log.Error("Request to get genres for filtering is failed, there are no data");
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper
                   .Map<IReadOnlyList<Genre>, IReadOnlyList<GenreFilteringDto>>(result));
        }

        [HttpGet("filter-authors")]
        public async Task<ActionResult<AuthorFilteringDto>> GetFilteringAuthorsAsync()
        {
            var result = await _authorRepo.GetListAllAsync();
            if (result == null)
            {
                Log.Error("Request to get authors for filtering is failed, there are no data");
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper
                   .Map<IReadOnlyList<Author>, IReadOnlyList<AuthorFilteringDto>>(result));
        }

        [HttpGet("filter-bookLanguage")]
        public async Task<ActionResult<BookLanguageFilteringDto>> GetFilteringBookLanguagesAsync()
        {
            var result = await _bookLanguageRepo.GetListAllAsync();
            if (result == null)
            {
                Log.Error("Request to get book languages for filtering is failed, there are no data");
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper
                   .Map<IReadOnlyList<BookLanguage>, IReadOnlyList<BookLanguageFilteringDto>>(result));
        }

        [HttpGet("filter-bookSeries")]
        public async Task<ActionResult<BookSeriesFilteringDto>> GetFilteringBookSeriesAsync()
        {
            var result = await _bookSeriesRepo.GetListAllAsync();
            if (result == null)
            {
                Log.Error("Request to get bookSeries for filtering is failed, there are no data");
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper
                   .Map<IReadOnlyList<BookSeries>, IReadOnlyList<BookSeriesFilteringDto>>(result));
        }

        [HttpGet("filter-narrator")]
        public async Task<ActionResult<NarratorFilteringDto>> GetFilteringNarratorsAsync()
        {
            var result = await _narratorRepo.GetListAllAsync();
            if (result == null)
            {
                Log.Error("Request to get narrators for filtering is failed, there are no data");
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper
                   .Map<IReadOnlyList<Narrator>, IReadOnlyList<NarratorFilteringDto>>(result));
        }
    }
}
