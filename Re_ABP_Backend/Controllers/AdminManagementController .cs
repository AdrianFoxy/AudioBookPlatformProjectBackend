using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Helpers;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Data.Specification.Params;
using Re_ABP_Backend.Errors;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.GenreDtos;
using Re_ABP_Backend.Data.Specification.SpecClasses.AdminGenreSpec;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Localization;
using Re_ABP_Backend.Resources;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.NarratorDtos;
using Re_ABP_Backend.Data.Specification.SpecClasses.AdminNarratorSpec;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.BookSeriesDtos;
using Re_ABP_Backend.Data.Specification.SpecClasses.AdminBookSeriesSpec;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminManagementController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;

        public AdminManagementController(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResource> sharedResourceLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sharedResourceLocalizer = sharedResourceLocalizer;
        }

        [HttpGet("genres")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<GenreDto>>> GetGenresList([FromQuery] PagAndSearchParams pagAndSearchParams)
        {
            var spec = new GenreSpecification(pagAndSearchParams);
            var countSpec = new GenreCountSpecification(pagAndSearchParams);

            var totalItems = await _unitOfWork.Repository<Genre>().CountAsync(countSpec);
            var items = await _unitOfWork.Repository<Genre>().GetListWithSpecAsync(spec);

            var data = _mapper
                .Map<IReadOnlyList<Genre>, IReadOnlyList<GenreDto>>(items);

            return Ok(new Pagination<GenreDto>(pagAndSearchParams.PageIndex, pagAndSearchParams.PageSize, totalItems, data));
        }

        [HttpGet("narrators")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<NarratorDto>>> GetNarratorsList([FromQuery] PagAndSearchParams pagAndSearchParams)
        {
            var spec = new NarratorSpecification(pagAndSearchParams);
            var countSpec = new NarratorCountSpecification(pagAndSearchParams);

            var totalItems = await _unitOfWork.Repository<Narrator>().CountAsync(countSpec);
            var items = await _unitOfWork.Repository<Narrator>().GetListWithSpecAsync(spec);

            var data = _mapper
                .Map<IReadOnlyList<Narrator>, IReadOnlyList<NarratorDto>>(items);

            return Ok(new Pagination<NarratorDto>(pagAndSearchParams.PageIndex, pagAndSearchParams.PageSize, totalItems, data));
        }

        [HttpGet("bookSeries")]
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

        [HttpGet("genre/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenreDto>> GetGenreById(int id)
        {
            var item = await _unitOfWork.Repository<Genre>().GetByIdAsync(id);

            if (item == null)
            {
                Log.Error("Error to get genre by id. Genre id: {id} not found", id);
                return NotFound();
            }

            var data = _mapper
                .Map<Genre, GenreDto>(item);

            return Ok(data);
        }

        [HttpGet("narrator/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NarratorDto>> GetNarratorById(int id)
        {
            var item = await _unitOfWork.Repository<Narrator>().GetByIdAsync(id);

            if (item == null)
            {
                Log.Error("Error to get narrator by id. Narrator id: {id} not found", id);
                return NotFound();
            }

            var data = _mapper
                .Map<Narrator, NarratorDto>(item);

            return Ok(data);
        }

        [HttpGet("bookSeries/{id}")]
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

        [HttpPost("add-genre")]
        public async Task<ActionResult<GenreDto>> CreateGenre(AddGenreDto addGenreDto)
        {
            try
            {
                var item = _mapper.Map<AddGenreDto, Genre>(addGenreDto);
                _unitOfWork.Repository<Genre>().Add(item);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem creating new genre.");
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemCreatingGenre")));
                }

                return Ok(item);
            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolationException(ex))
            {
                Log.Error("Genre with this Name or EnName already exists.");
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("UniqGenre")));
            }
        }

        [HttpPost("add-narrator")]
        public async Task<ActionResult<NarratorDto>> CreateNarrator(AddNarratorDto addNarratorDto)
        {
            try
            {
                var item = _mapper.Map<AddNarratorDto, Narrator>(addNarratorDto);
                _unitOfWork.Repository<Narrator>().Add(item);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem creating new narrator.");
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemCreatingNarrator")));
                }

                return Ok(item);
            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolationException(ex))
            {
                Log.Error("Narrator with this Name already exists.");
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("UniqNarrator")));
            }
        }

        [HttpPost("add-bookSeries")]
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


        [HttpPut("genre/{id}")]
        public async Task<ActionResult<GenreDto>> UpdateGenre(int id, AddGenreDto genreToUpdate)
        {
            try
            {
                var item = await _unitOfWork.Repository<Genre>().GetByIdAsync(id);

                if (item == null)
                {
                    Log.Error("Error to get genre by id. Genre id: {id} not found", id);
                    return NotFound();
                }

                _mapper.Map(genreToUpdate, item);
                _unitOfWork.Repository<Genre>().Update(item);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem updating genre. Genre id: {id}", id);
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemUpdatingGenre")));
                }

                return Ok(item);
            }
            catch (DbUpdateException ex)
            {
                if (IsUniqueConstraintViolationException(ex))
                {
                    Log.Error("Genre with this Name or EnName already exists.");
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("UniqGenre")));
                }
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemUpdatingGenre")));
            }
        }

        [HttpPut("narrator/{id}")]
        public async Task<ActionResult<NarratorDto>> UpdateNarrator(int id, AddNarratorDto narratorToUpdate)
        {
            try
            {
                var item = await _unitOfWork.Repository<Narrator>().GetByIdAsync(id);

                if (item == null)
                {
                    Log.Error("Error to get narrator by id. Narrator id: {id} not found", id);
                    return NotFound();
                }

                _mapper.Map(narratorToUpdate, item);
                _unitOfWork.Repository<Narrator>().Update(item);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem updating narrator. Narrator id: {id}", id);
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemCreatingNarrator")));
                }

                return Ok(item);
            }
            catch (DbUpdateException ex)
            {
                if (IsUniqueConstraintViolationException(ex))
                {
                    Log.Error("Narrator with this Name already exists.");
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("UniqNarrator")));
                }
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemCreatingNarrator")));
            }
        }

        [HttpPut("bookSeries/{id}")]
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

        [HttpDelete("delete-genre/{id}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            try
            {
                var item = await _unitOfWork.Repository<Genre>().GetByIdAsync(id);

                if (item == null)
                {
                    Log.Error("Error deleting genre. Genre id: {id} not found", id);
                    return NotFound();
                }

                _unitOfWork.Repository<Genre>().Delete(item);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem deleting genre. Genre id: {id}", id);
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemDeletingGenre")));
                }
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "Error deleting genre. Genre id: {id}", id);
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemDeletingGenreAssociated")));
            }
        }

        [HttpDelete("delete-narrator/{id}")]
        public async Task<ActionResult> DeleteNarrator(int id)
        {
            try
            {
                var item = await _unitOfWork.Repository<Narrator>().GetByIdAsync(id);

                if (item == null)
                {
                    Log.Error("Error deleting narrator. Narrator id: {id} not found", id);
                    return NotFound();
                }

                _unitOfWork.Repository<Narrator>().Delete(item);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem deleting narrator. Narrator id: {id}", id);
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemDeletingNarrator")));
                }
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "Error deleting narrator. Narrator id: {id}", id);
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemDeletingNarratorAssociated")));
            }
        }

        [HttpDelete("delete-bookSeries/{id}")]
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
