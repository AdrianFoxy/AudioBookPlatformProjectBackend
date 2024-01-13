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

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminManagementController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminManagementController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("genres")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<GenreDto>>> GetGenresList([FromQuery] PagAndSearchParams pagAndSearchParams)
        {
            var spec = new GenreSpecification(pagAndSearchParams);
            var countSpec = new GenreCountSpecification(pagAndSearchParams);

            var totalItems = await _unitOfWork.Repository<Genre>().CountAsync(countSpec);
            var genres = await _unitOfWork.Repository<Genre>().GetListWithSpecAsync(spec);

            var data = _mapper
                .Map<IReadOnlyList<Genre>, IReadOnlyList<GenreDto>>(genres);

            return Ok(new Pagination<GenreDto>(pagAndSearchParams.PageIndex, pagAndSearchParams.PageSize, totalItems, data));
        }

        [HttpGet("genre/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenreDto>> GetGenreById(int id)
        {
            var genre = await _unitOfWork.Repository<Genre>().GetByIdAsync(id);
            var data = _mapper
                .Map<Genre, GenreDto>(genre);

            return Ok(data);
        }

        [HttpPost("add-genre")]
        public async Task<ActionResult<GenreDto>> CreateGenre(AddGenreDto addGenreDto)
        {
            try
            {
                var genre = _mapper.Map<AddGenreDto, Genre>(addGenreDto);

                _unitOfWork.Repository<Genre>().Add(genre);
                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem creating new genre.");
                    return BadRequest(new ApiResponse(400, "Problem creating genre"));
                }

                return Ok(genre);
            }
            catch (DbUpdateException ex)
            {
                if (IsUniqueConstraintViolationException(ex))
                {
                    Log.Error("Genre with this Name or EnName already exists.");
                    return BadRequest(new ApiResponse(400, "Genre with this Name or EnName already exists"));
                }

                return BadRequest(new ApiResponse(400, "Problem creating genre"));
            }
        }

        [HttpPut("genre/{id}")]
        public async Task<ActionResult<GenreDto>> UpdateGenre(int id, AddGenreDto genreToUpdate)
        {
            try
            {
                var genre = await _unitOfWork.Repository<Genre>().GetByIdAsync(id);

                _mapper.Map(genreToUpdate, genre);
                _unitOfWork.Repository<Genre>().Update(genre);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem updating genre. Genre id: {id}", id);
                    return BadRequest(new ApiResponse(400, "Problem updating genre"));
                }

                return Ok(genre);
            }
            catch (DbUpdateException ex)
            {
                if (IsUniqueConstraintViolationException(ex))
                {
                    Log.Error("Genre with this Name or EnName already exists.");
                    return BadRequest(new ApiResponse(400, "Genre with this Name or EnName already exists"));
                }
                return BadRequest(new ApiResponse(400, "Problem updating genre"));

            }
        }

        [HttpDelete("delete-genre/{id}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            try
            {
                var genre = await _unitOfWork.Repository<Genre>().GetByIdAsync(id);

                if (genre == null)
                {
                    Log.Error("Error deleting genre. Genre id: {id} not found", id);
                    return NotFound();
                }

                _unitOfWork.Repository<Genre>().Delete(genre);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem deleting genre. Genre id: {id}", id);
                    return BadRequest(new ApiResponse(400, "Problem deleting genre"));
                }

                return Ok();
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "Error deleting genre. Genre id: {id}", id);
                return BadRequest(new ApiResponse(400, "Genre is associated with audio books and cannot be deleted."));
            }
        }


        private bool IsUniqueConstraintViolationException(DbUpdateException ex)
        {
            return ex?.InnerException is SqlException sqlException &&
                   (sqlException.Number == 2601 || sqlException.Number == 2627);
        }

    }
}
