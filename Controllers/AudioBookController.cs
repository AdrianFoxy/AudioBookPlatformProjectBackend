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
        private readonly IAudioBookRepository _audioBookRepository;
        public AudioBookController(IAudioBookRepository audioBookRepositor)
        { 
            _audioBookRepository = audioBookRepositor;
        }

        [HttpGet]
        public async Task<ActionResult<List<AudioBook>>> GetBooks()
        {
            var abooks = await _audioBookRepository.GetAudioBooksAsync();
            return Ok(abooks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AudioBook>> GetBook(int id)
        {
            return await _audioBookRepository.GetAudioBookByIdAsync(id);
        }
    }
}
