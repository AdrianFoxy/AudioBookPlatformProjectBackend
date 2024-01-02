using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Re_ABP_Backend.Data.Dtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Data.Specification.SpecClasses.AudioBooks;
using Re_ABP_Backend.Errors;
using Serilog;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRecommendationRepository _recommendationRepository;
        public RecommendationController(IMapper mapper,
                                        IRecommendationRepository recommendationRepository)
        {
            _mapper = mapper;
            _recommendationRepository = recommendationRepository;
        }

        [HttpGet("recentlyWatched")]
        public async Task<ActionResult<AudioBookInLibraryDto>> GetAudioBookRecentlyWatched([FromQuery] List<int> audioBooksIds)
        {
            var result = await _recommendationRepository.GetRecentlyWatched(audioBooksIds);
            if (result == null)
            {
                Log.Error("Request to get recommedation is failed, there is no data");
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper
                   .Map<IReadOnlyList<AudioBook>, IReadOnlyList<AudioBookInLibraryDto>>(result));
        }

        [HttpGet("byPopularity")]
        public async Task<ActionResult<AudioBookInLibraryDto>> GetRecommedantionByPopularityAsync()
        {
            var result = await _recommendationRepository.GetRecommendationsByPopularity();
            if (result == null)
            {
                Log.Error("Request to get recommedation is failed, there is no data");
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper
                   .Map<IReadOnlyList<AudioBook>, IReadOnlyList<AudioBookInLibraryDto>>(result));
        }

        [HttpGet("byRating")]
        public async Task<ActionResult<AudioBookInLibraryDto>> GetRecommedantionByRatingAsync()
        {
            var result = await _recommendationRepository.GetRecommendationsByRating();
            if (result == null)
            {
                Log.Error("Request to get recommedation is failed, there is no data");
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper
                   .Map<IReadOnlyList<AudioBook>, IReadOnlyList<AudioBookInLibraryDto>>(result));
        }
    }
}
