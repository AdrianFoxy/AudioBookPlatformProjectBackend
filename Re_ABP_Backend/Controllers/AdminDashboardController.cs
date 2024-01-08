using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Interfraces;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDashboard _dashboard;


        public AdminDashboardController(IUserService userService, IMapper mapper, IUnitOfWork unitOfWork, IDashboard dashboard)
        {

            _userService = userService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _dashboard = dashboard;
        }

        [HttpGet("audiobook-count")]
        public async Task<IActionResult> AudiobookCount()
        {
            var count = await _unitOfWork.Repository<AudioBook>().CountAsyncWithSpec();
            return Ok(count);
        }

        [HttpGet("review-count")]
        public async Task<IActionResult> ReviewCount()
        {
            var count = await _unitOfWork.Repository<Review>().CountAsyncWithSpec();
            return Ok(count);
        }

        [HttpGet("user-count")]
        public async Task<IActionResult> UserCount()
        {
            var count = await _userService.UserCountAsync();
            return Ok(count);
        }

        [HttpGet("user-count-chart")]
        public async Task<IActionResult> UserCountChart()
        {
            var count = await _dashboard.GetUserCountByMothAsync();
            return Ok(count);
        }
    }
}
