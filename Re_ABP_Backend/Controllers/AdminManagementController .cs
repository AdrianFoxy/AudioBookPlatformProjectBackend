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
            catch
            {
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemCreatingGenre")));
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
            catch
            {
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemCreatingNarrator")));
            }
        }


        [HttpPut("genre/{id}")]
        public async Task<ActionResult<GenreDto>> UpdateGenre(int id, AddGenreDto genreToUpdate)
        {
            try
            {
                var item = await _unitOfWork.Repository<Genre>().GetByIdAsync(id);

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
            catch
            {
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemUpdatingGenre")));
            }

        }

        [HttpPut("narrator/{id}")]
        public async Task<ActionResult<NarratorDto>> UpdateNarrator(int id, AddNarratorDto narratorToUpdate)
        {
            try
            {
                var item = await _unitOfWork.Repository<Narrator>().GetByIdAsync(id);

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
            catch
            {
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemCreatingNarrator")));
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
            catch
            {
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemDeletingGenre")));
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
                    Log.Error("Problem deleting narrtor. Narrator id: {id}", id);
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemDeletingNarrator")));
                }
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "Error deleting genre. Genre id: {id}", id);
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemDeletingNarratorAssociated")));
            }
            catch
            {
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemDeletingNarrator")));
            }
        }


        private bool IsUniqueConstraintViolationException(DbUpdateException ex)
        {
            return ex?.InnerException is SqlException sqlException &&
                   (sqlException.Number == 2601 || sqlException.Number == 2627);
        }

    }
}
