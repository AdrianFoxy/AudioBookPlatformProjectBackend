using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Re_ABP_Backend.Data.Dtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Helpers;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Data.Specification.Params;
using Re_ABP_Backend.Data.Specification.SpecClasses.AudioBookSpec.Count;
using Re_ABP_Backend.Data.Specification.SpecClasses.AudioBookSpec;
using Re_ABP_Backend.Errors;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.GenreDtos;
using Re_ABP_Backend.Data.Specification.SpecClasses.AdminGenreSpec;

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
    }
}
