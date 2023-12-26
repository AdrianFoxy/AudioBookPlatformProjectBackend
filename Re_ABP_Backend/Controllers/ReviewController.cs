using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Re_ABP_Backend.Data.Dtos.ReviewDtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Helpers;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Data.Specification.SpecClasses;
using Re_ABP_Backend.Data.Specification.SpecClasses.AudioBooks;
using Re_ABP_Backend.Data.Specification.SpecClasses.ReviewSpec;
using Re_ABP_Backend.Errors;
using Serilog;
using System.Security.Claims;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ReviewController(IUnitOfWork unitOfWork,
                                IMapper mapper,
                                IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ReviewDto>>> GetReviews([FromQuery] ABOfSomethingParams paginationParams)
        {
            var spec = new ReviewSpecification(paginationParams);
            var countSpec = new ReviewCountSpecification(paginationParams);

            var totalItems = await _unitOfWork.Repository<Review>().CountAsync(countSpec);

            var reviews = await _unitOfWork.Repository<Review>().GetListWithSpecAsync(spec);
  
            var data = _mapper
              .Map<IReadOnlyList<Review>, IReadOnlyList<ReviewDto>>(reviews);

            return Ok(new Pagination<ReviewDto>(paginationParams.PageIndex,
                paginationParams.PageSize, totalItems, data));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ReviewDto>> CreateReview(ReviewCreateDto createReviewDto)
        {
            var reviewExists = await _userService.NewReviewAllowed(createReviewDto.AudioBookId, createReviewDto.UserId);
            if (reviewExists)
            {
                Log.Error("Problem creating review. UserId: {createReviewDto.UserId} alredy has a review for this audiobook.", createReviewDto.UserId);
                return BadRequest(new ApiResponse(400, "Problem creating review. The user alredy has a review."));
            }

            var review = _mapper.Map<ReviewCreateDto, Review>(createReviewDto);

            _unitOfWork.Repository<Review>().Add(review);

            var result = await _unitOfWork.Complete();

            var username = await _userService.GetUserById(createReviewDto.UserId);
            review.User.UserName = username.UserName;

            if (result <= 0)
            {
                Log.Error("Problem creating review. UserId: {createReviewDto.UserId}.", createReviewDto.UserId);
                return BadRequest(new ApiResponse(400, "Problem creating review"));
            }

            return Ok(_mapper.
                Map<Review, ReviewDto>(review));
        }

        [Authorize]
        [HttpPut("id")]
        public async Task<ActionResult<ReviewDto>> UpdateReview(int id, ReviewCreateDto reviewToUpdate)
        {
            var review = await _unitOfWork.Repository<Review>().GetByIdAsync(id);

            var userIdentifier = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdentifier != review.UserId.ToString())
                return BadRequest(new ApiResponse(400, "This is not your review, or you cannot update it."));

            _mapper.Map(reviewToUpdate, review);
            _unitOfWork.Repository<Review>().Update(review);

            var result = await _unitOfWork.Complete();

            var username = await _userService.GetUserById(reviewToUpdate.UserId);
            review.User.UserName = username.UserName;

            if (result <= 0) 
            {
                Log.Error("Problem creating review. UserId: {reviewToUpdate.UserId}. Review id: {id}", reviewToUpdate.UserId, id);
                return BadRequest(new ApiResponse(400, "Problem updateing review"));
            } 

            return Ok(_mapper.
                Map<Review, ReviewDto>(review));
        }

        [Authorize]
        [HttpDelete("id")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            
           var review = await _unitOfWork.Repository<Review>().GetByIdAsync(id);

           var userIdentifier = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
           var userRole = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            if (userIdentifier != review.UserId.ToString() && userRole != "Admin")
                return BadRequest(new ApiResponse(400, "This is not your review, or you cannot delete it."));

            _unitOfWork.Repository<Review>().Delete(review);

            var result = await _unitOfWork.Complete();

            if (result <= 0) 
            {
                Log.Error("Problem deleting review. Review id: {id}", id);
                return BadRequest(new ApiResponse(400, "Problem deleting review"));
            }

            return Ok();
        }
    }
}
