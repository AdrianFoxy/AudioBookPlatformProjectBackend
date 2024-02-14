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
using Microsoft.AspNetCore.Authorization;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminManagementGenreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;

        public AdminManagementGenreController(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResource> sharedResourceLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sharedResourceLocalizer = sharedResourceLocalizer;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
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
            catch (DbUpdateException ex) when (SQLExceptionHandler.IsUniqueConstraintViolationException(ex))
            {
                return SQLExceptionHandler.HandleDbUniqException(ex, _sharedResourceLocalizer, "UniqGenre");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
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
            catch (DbUpdateException ex) when (SQLExceptionHandler.IsUniqueConstraintViolationException(ex))
            {
                return SQLExceptionHandler.HandleDbUniqException(ex, _sharedResourceLocalizer, "UniqGenre");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
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
    }
}
