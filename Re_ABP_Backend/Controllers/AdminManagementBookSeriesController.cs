using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.BookSeriesDtos;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.NarratorDtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Helpers;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Data.Specification.Params;
using Re_ABP_Backend.Data.Specification.SpecClasses.AdminBookSeriesSpec;
using Re_ABP_Backend.Errors;
using Re_ABP_Backend.Resources;
using Serilog;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminManagementBookSeriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;

        public AdminManagementBookSeriesController(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResource> sharedResourceLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sharedResourceLocalizer = sharedResourceLocalizer;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<BookSeriesDto>>> GetBookSeriesList([FromQuery] PagAndSearchParams pagAndSearchParams)
        {
            var spec = new BookSeriesSpecification(pagAndSearchParams);
            var countSpec = new BookSeriesCountSpecification(pagAndSearchParams);

            var totalItems = await _unitOfWork.Repository<BookSeries>().CountAsync(countSpec);
            var items = await _unitOfWork.Repository<BookSeries>().GetListWithSpecAsync(spec);

            var data = _mapper
                .Map<IReadOnlyList<BookSeries>, IReadOnlyList<BookSeriesDto>>(items);

            return Ok(new Pagination<BookSeriesDto>(pagAndSearchParams.PageIndex, pagAndSearchParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookSeriesDto>> GetBookSeriesById(int id)
        {
            var item = await _unitOfWork.Repository<BookSeries>().GetByIdAsync(id);

            if (item == null)
            {
                Log.Error("Error to get book series by id. Book series id: {id} not found", id);
                return NotFound();
            }

            var data = _mapper
                .Map<BookSeries, BookSeriesDto>(item);

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<BookSeriesDto>> CreateBookSeries(AddBookSeriesDto addBookSeriesDto)
        {
            try
            {
                var item = _mapper.Map<AddBookSeriesDto, BookSeries>(addBookSeriesDto);
                _unitOfWork.Repository<BookSeries>().Add(item);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem creating new book series.");
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemCreatingBookSeries")));
                }

                return Ok(item);
            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolationException(ex))
            {
                Log.Error("Book series with this Name or enName already exists.");
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("UniqBookSeries")));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<NarratorDto>> UpdateBookSeries(int id, AddBookSeriesDto bookSeriesToUpdate)
        {
            try
            {
                var item = await _unitOfWork.Repository<BookSeries>().GetByIdAsync(id);

                if (item == null)
                {
                    Log.Error("Error to get bookSeries by id. BookSeries id: {id} not found", id);
                    return NotFound();
                }

                _mapper.Map(bookSeriesToUpdate, item);
                _unitOfWork.Repository<BookSeries>().Update(item);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem updating bookseries. BookSeries id: {id}", id);
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemUpdatingBookSeries")));
                }

                return Ok(item);
            }
            catch (DbUpdateException ex)
            {
                if (IsUniqueConstraintViolationException(ex))
                {
                    Log.Error("Bookseries with this Name or enName already exists.");
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("UniqBookSeries")));
                }
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemUpdatingBookSeries")));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBookSeries(int id)
        {
            try
            {
                var item = await _unitOfWork.Repository<BookSeries>().GetByIdAsync(id);

                if (item == null)
                {
                    Log.Error("Error deleting bookseries. BooksSeries id: {id} not found", id);
                    return NotFound();
                }

                _unitOfWork.Repository<BookSeries>().Delete(item);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem deleting bookseries. BooksSeries id: {id}", id);
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemDeletingBookSeries")));
                }
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "Error deleting bookseries. BooksSeries id: {id}", id);
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemDeletingBookSeriesAssociated")));
            }
        }

        private bool IsUniqueConstraintViolationException(DbUpdateException ex)
        {
            return ex?.InnerException is SqlException sqlException &&
                   (sqlException.Number == 2601 || sqlException.Number == 2627);
        }
    }
}
