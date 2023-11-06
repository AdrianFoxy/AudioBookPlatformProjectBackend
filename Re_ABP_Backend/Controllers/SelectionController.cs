using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Re_ABP_Backend.Data.Dtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Helpers;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Data.Specification.SpecClasses;
using Re_ABP_Backend.Errors;
using Serilog;
using Re_ABP_Backend.Data.Specification.SpecClasses.SelectionSpec;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SelectionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SelectionController(IUnitOfWork unitOfWork, 
                                   IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SingleSelectionDto>>> GetSelectionAsync()
        {
            var selection = await _unitOfWork.Repository<BookSelection>().GetListAllAsync();

            if (selection.Count == 0)
            {
                Log.Error("Request to get selections has failed, there is no data.");
                return NotFound(new ApiResponse(404));
            }

            var selectionDtos = selection.Select(s => _mapper.Map<BookSelection, SingleSelectionDto>(s)).ToList();

            return Ok(selectionDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Author>> GetBookSelection(int id)
        {
            var selection = await _unitOfWork.Repository<BookSelection>().GetByIdAsync(id);

            if (selection == null)
            {
                Log.Error("Request to get selection by id failed, selection with id {Id} does not exists.", id);
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper
                    .Map<BookSelection, SingleSelectionDto>(selection));
        }


        [HttpGet("selection-books")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AudioBookInLibraryDto>> GetBooksOfSelection([FromQuery] ABOfSomethingParams abParams)
        {
            var spec = new BooksOfSelectsSpecification(abParams);
            var countSpec = new BooksOfSelectsCountSpecification(abParams);

            var totalItems = await _unitOfWork.Repository<AudioBook>().CountAsync(countSpec);

            var abooks = await _unitOfWork.Repository<AudioBook>().GetListWithSpecAsync(spec);
            if (abooks.Count == 0)
            {
                Log.Error("Request to get audiobooks by selection {id} is failed, there is no data", abParams.Id);
                return NotFound(new ApiResponse(404));
            }

            var data = _mapper
                .Map<IReadOnlyList<AudioBook>, IReadOnlyList<AudioBookInLibraryDto>>(abooks);

            return Ok(new Pagination<AudioBookInLibraryDto>(abParams.PageIndex,
                abParams.PageSize, totalItems, data));

        }
    }
}
