using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.NarratorDtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Helpers;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Data.Specification.Params;
using Re_ABP_Backend.Data.Specification.SpecClasses.AdminNarratorSpec;
using Re_ABP_Backend.Errors;
using Re_ABP_Backend.Resources;
using Serilog;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminManagementNarratorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;

        public AdminManagementNarratorController(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResource> sharedResourceLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sharedResourceLocalizer = sharedResourceLocalizer;
        }

        [HttpGet]
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

        [HttpGet("{id}")]
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

        [HttpPost]
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
            catch (DbUpdateException ex) when (SQLExceptionHandler.IsUniqueConstraintViolationException(ex))
            {
                Log.Error("Narrator with this Name already exists.");
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("UniqNarrator")));
            }
        }

        [HttpPut("{id}")]
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
            catch (DbUpdateException ex) when (SQLExceptionHandler.IsUniqueConstraintViolationException(ex))
            {
                Log.Error("Narrator with this Name already exists.");
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("UniqNarrator")));
            }
        }

        [HttpDelete("{id}")]
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
    }
}
