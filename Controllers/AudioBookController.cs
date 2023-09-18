using ABP_Backend.Data;
using ABP_Backend.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioBookController : ControllerBase
    {
        private readonly AppDBContext _context;
        public AudioBookController(AppDBContext context)
        { 
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<AudioBook>>> GetBooks()
        {
            var abooks = await _context.AudioBook.ToListAsync();
            return abooks;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AudioBook>> GetBook(int id)
        {
            return await _context.AudioBook.FindAsync(id);
        }
    }
}
