using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.AuthorDtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Entities.Picture;
using Re_ABP_Backend.Data.Helpers;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Data.Specification.Params;
using Re_ABP_Backend.Data.Specification.SpecClasses.AdminAuthor;
using Re_ABP_Backend.Errors;
using Re_ABP_Backend.Resources;
using Serilog;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminManagementAuthor : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPictureService _pictureService;
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;

        public AdminManagementAuthor(IMapper mapper, IUnitOfWork unitOfWork, IPictureService pictureService, IStringLocalizer<SharedResource> sharedResourceLocalizer)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _pictureService = pictureService;
            _sharedResourceLocalizer = sharedResourceLocalizer;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<AuthorDto>>> GetAuthorsList([FromQuery] PagAndSearchParams pagAndSearchParams)
        {
            var spec = new AuthorSpecification(pagAndSearchParams);
            var countSpec = new AuthorCountSpecification(pagAndSearchParams);

            var totalItems = await _unitOfWork.Repository<Author>().CountAsync(countSpec);
            var items = await _unitOfWork.Repository<Author>().GetListWithSpecAsync(spec);

            var data = _mapper
                .Map<IReadOnlyList<Author>, IReadOnlyList<AuthorDto>>(items);

            return Ok(new Pagination<AuthorDto>(pagAndSearchParams.PageIndex, pagAndSearchParams.PageSize, totalItems, data));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthorDto>> GetGenreById(int id)
        {
            var item = await _unitOfWork.Repository<Author>().GetByIdAsync(id);

            if (item == null)
            {
                Log.Error("Error to get author by id. Author id: {id} not found", id);
                return NotFound();
            }

            var data = _mapper
                .Map<Author, AuthorDto>(item);

            return Ok(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Author>> AddAuthor([FromForm] AddAuthorDto addAuthorDto)
        {
            try
            {
                if (addAuthorDto.Picture == null || addAuthorDto.Picture.Length == 0)
                {
                    return BadRequest("The picture field is required.");
                }

                var picture = await _pictureService.SaveToDiskAsync(addAuthorDto.Picture, PictureType.authors);

                if (picture == null)
                {
                    ModelState.AddModelError(nameof(addAuthorDto.Picture), "Error during saving picture.");
                    return BadRequest(ModelState);
                }

                var item = _mapper.Map<AddAuthorDto, Author>(addAuthorDto);

                item.ImageUrl = picture.PictureUrl;

                _unitOfWork.Repository<Author>().Add(item);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem creating new author.");
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemCreatingAuthor")));
                }

                return Ok(item);
            }
            catch (DbUpdateException ex) when (SQLExceptionHandler.IsUniqueConstraintViolationException(ex))
            {
                return SQLExceptionHandler.HandleDbUniqException(ex, _sharedResourceLocalizer, "UniqAuthor");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDto>> UpdateAuthor(int id, [FromForm] AddAuthorDto authorToUpdate)
        {
            try
            {
                var item = await _unitOfWork.Repository<Author>().GetByIdAsync(id);

                if (item == null)
                {
                    Log.Error("Error to get author by id. Author id: {id} not found", id);
                    return NotFound();
                }

                if(item.ImageUrl != null && authorToUpdate.Picture != null)
                {
                    var picture = await _pictureService.SaveToDiskAsync(authorToUpdate.Picture, PictureType.authors);
                    if (picture == null)
                    {
                        ModelState.AddModelError(nameof(authorToUpdate.Picture), "Error during saving picture.");
                        return BadRequest(ModelState);
                    }
                    _pictureService.DeleteFromDisk(Path.GetFileName(item.ImageUrl), PictureType.authors);
                    item.ImageUrl = picture.PictureUrl;
                }

                _mapper.Map(authorToUpdate, item);

                _unitOfWork.Repository<Author>().Update(item);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem updating author. Author id: {id}", id);
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemUpdatingAuthor")));
                }

                return Ok(item);
            }
            catch (DbUpdateException ex) when (SQLExceptionHandler.IsUniqueConstraintViolationException(ex))
            {
                return SQLExceptionHandler.HandleDbUniqException(ex, _sharedResourceLocalizer, "UniqAuthor");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            try
            {
                var item = await _unitOfWork.Repository<Author>().GetByIdAsync(id);

                if (item == null)
                {
                    Log.Error("Error deleting author. Author id: {id} not found", id);
                    return NotFound();
                }

                _unitOfWork.Repository<Author>().Delete(item);
                _pictureService.DeleteFromDisk(Path.GetFileName(item.ImageUrl), PictureType.authors);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem deleting author. Author id: {id}", id);
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemDeletingAuthor")));
                }

                return Ok();
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "Error deleting author. Author id: {id}", id);
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemDeletingAuthorAssociated")));
            }
        }

    }
}
