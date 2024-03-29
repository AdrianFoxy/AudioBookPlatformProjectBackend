﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Errors;

namespace Re_ABP_Backend.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDashboardService _dashboard;


        public AdminDashboardController(IUserService userService, IMapper mapper, IUnitOfWork unitOfWork, IDashboardService dashboard)
        {

            _userService = userService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _dashboard = dashboard;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("audiobook-count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AudiobookCount()
        {
            var count = await _unitOfWork.Repository<AudioBook>().CountAsyncWithSpec();
            return Ok(count);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("review-count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReviewCount()
        {
            var count = await _unitOfWork.Repository<Review>().CountAsyncWithSpec();
            return Ok(count);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("user-count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UserCount()
        {
            var count = await _userService.UserCountAsync();
            return Ok(count);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("user-count-chart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UserCountChart()
        {
            var count = await _dashboard.GetUserCountByMothAsync();
            return Ok(count);
        }
    }
}
