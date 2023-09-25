﻿using ABP_Backend.Data.DB;
using ABP_Backend.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly AppDBContext _context;

        public BuggyController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet("not-found")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _context.AudioBook.Find(123123123);
            if(thing == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok();
        }

        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
            var thing = _context.AudioBook.Find(123123123);
            var thingToReturn = thing.ToString();

            return Ok();
        }

        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetValidationError(int id)
        {
            return Ok();
        }

    }
}
