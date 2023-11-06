using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Re_ABP_Backend.Data.Dtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Helpers;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Data.Specification.SpecClasses;
using Re_ABP_Backend.Data.Specification.SpecClasses.AudioBookSpec;
using Re_ABP_Backend.Data.Specification.SpecClasses.AuthorSpec;
using Re_ABP_Backend.Errors;
using Serilog;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public AuthorController(IUnitOfWork unitOfWork,
                                IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Author>> GetAuthorAsync(int id)
        {
            var spec = new AuthorSpecification(id);
            var author = await _unitOfWork.Repository<Author>().GetEntityWithSpec(spec);

            if (author == null)
            {
                Log.Error("Request to get author by id failed, author with id {Id} does not exists.", id);
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper
                    .Map<Author, SingleAuthorDto>(author));
        }

        [HttpGet("author-books")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AudioBookInLibraryDto>> GetBooksOfAuthor([FromQuery] ABOfSomethingParams abParams)
        {
            var spec = new AuthorBooksSpecification(abParams);
            var countSpec = new AuthorBooksCountSpecification(abParams);

            var totalItems = await _unitOfWork.Repository<AudioBook>().CountAsync(countSpec);

            var abooks = await _unitOfWork.Repository<AudioBook>().GetListWithSpecAsync(spec);
            if (abooks.Count == 0)
            {
                Log.Error("Request to get audiobooks by author {id} is failed, there is no data", abParams.Id);
                return NotFound(new ApiResponse(404));
            }

            var data = _mapper
                .Map<IReadOnlyList<AudioBook>, IReadOnlyList<AudioBookInLibraryDto>>(abooks);

            return Ok(new Pagination<AudioBookInLibraryDto>(abParams.PageIndex,
                abParams.PageSize, totalItems, data));

        }
    }
}
