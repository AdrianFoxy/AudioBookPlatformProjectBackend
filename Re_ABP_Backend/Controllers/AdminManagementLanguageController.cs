using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.LanguageDtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Helpers;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Data.Specification.Params;
using Re_ABP_Backend.Data.Specification.SpecClasses.AdminBookLanguageSpec;
using Re_ABP_Backend.Errors;
using Re_ABP_Backend.Resources;
using Serilog;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminManagementLanguageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;

        public AdminManagementLanguageController(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResource> sharedResourceLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sharedResourceLocalizer = sharedResourceLocalizer;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<BookLanguageDto>>> GetBookLanguagesList([FromQuery] PagAndSearchParams pagAndSearchParams)
        {
            var spec = new BookLanguageSpecification(pagAndSearchParams);
            var countSpec = new BookLanguageCountSpecification(pagAndSearchParams);

            var totalItems = await _unitOfWork.Repository<BookLanguage>().CountAsync(countSpec);
            var items = await _unitOfWork.Repository<BookLanguage>().GetListWithSpecAsync(spec);

            var data = _mapper
                .Map<IReadOnlyList<BookLanguage>, IReadOnlyList<BookLanguageDto>>(items);

            return Ok(new Pagination<BookLanguageDto>(pagAndSearchParams.PageIndex, pagAndSearchParams.PageSize, totalItems, data));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookLanguageDto>> GetBookLanguagesById(int id)
        {
            var item = await _unitOfWork.Repository<BookLanguage>().GetByIdAsync(id);

            if (item == null)
            {
                Log.Error("Error to get book language by id. Book language id: {id} not found", id);
                return NotFound();
            }

            var data = _mapper
                .Map<BookLanguage, BookLanguageDto>(item);

            return Ok(data);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<BookLanguageDto>> CreateBookLanguage(AddBookLanguageDto addBookLanguage)
        {
            try
            {
                var item = _mapper.Map<AddBookLanguageDto, BookLanguage>(addBookLanguage);
                _unitOfWork.Repository<BookLanguage>().Add(item);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem creating new book language.");
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemCreatingBookLanguage")));
                }

                return Ok(item);
            }
            catch (DbUpdateException ex) when (SQLExceptionHandler.IsUniqueConstraintViolationException(ex))
            {
                Log.Error("BookLanguage with this Name or EnName already exists.");
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("UniqBookLanguage")));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<BookLanguageDto>> UpdateBookLanguage(int id, AddBookLanguageDto updateBookLanguage)
        {
            try
            {
                var item = await _unitOfWork.Repository<BookLanguage>().GetByIdAsync(id);

                if (item == null)
                {
                    Log.Error("Error to get book language by id. Book language id: {id} not found", id);
                    return NotFound();
                }

                _mapper.Map(updateBookLanguage, item);
                _unitOfWork.Repository<BookLanguage>().Update(item);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem updating book language. Book language id: {id}", id);
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemUpdatingBookLanguage")));
                }

                return Ok(item);
            }
            catch (DbUpdateException ex) when (SQLExceptionHandler.IsUniqueConstraintViolationException(ex))
            {
                Log.Error("Book language with this Name or EnName already exists.");
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("UniqBookLanguage")));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBookLanguage(int id)
        {
            try
            {
                var item = await _unitOfWork.Repository<BookLanguage>().GetByIdAsync(id);

                if (item == null)
                {
                    Log.Error("Error deleting book language. Book language id: {id} not found", id);
                    return NotFound();
                }

                _unitOfWork.Repository<BookLanguage>().Delete(item);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem deleting book language. Book language id: {id}", id);
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemDeletingBookLanguage")));
                }
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "Error deleting book language. Book language id: {id}", id);
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemDeletingBookLanguageAssociated")));
            }
        }

    }
}
