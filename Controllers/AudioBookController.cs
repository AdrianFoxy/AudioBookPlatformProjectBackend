using ABP_Backend.Data;
using ABP_Backend.Data.Entities;
using ABP_Backend.Data.Interfraces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioBookController : ControllerBase
    {
        private readonly IGenericRepository<AudioBook> _audioBookRepo;
        private readonly IGenericRepository<Genre> _genreRepo;

        public AudioBookController(IGenericRepository<AudioBook> audioBookRepo,
                                   IGenericRepository<Genre> genreRepo)
        {
            _audioBookRepo = audioBookRepo;
            _genreRepo = genreRepo;
        }

        [HttpGet("get-all-books")]
        public async Task<ActionResult<List<AudioBook>>> GetBooks()
        {
            var abooks = await _audioBookRepo.GetListAllAsync();
            return Ok(abooks);
        }

        [HttpGet("get-all-genres")]
        public async Task<ActionResult<List<Genre>>> GetGenres()
        {
            var genres = await _genreRepo.GetListAllAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AudioBook>> GetBook(int id)
        {
            return await _audioBookRepo.GetByIdAsync(id);
        }
    }
}
