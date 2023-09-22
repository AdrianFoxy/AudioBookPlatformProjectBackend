using ABP_Backend.Data;
using ABP_Backend.Data.Dtos;
using ABP_Backend.Data.Entities;
using ABP_Backend.Data.Interfraces;
using ABP_Backend.Data.Specification.SpecClasses;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public AudioBookController(IGenericRepository<AudioBook> audioBookRepo,
                                   IGenericRepository<Genre> genreRepo,
                                   IMapper mapper)
        {
            _audioBookRepo = audioBookRepo;
            _genreRepo = genreRepo;
            _mapper = mapper;
        }

        [HttpGet("get-all-books")]
        public async Task<ActionResult<IReadOnlyList<AudioBookInLibraryDto>>> GetBooks()
        {
            var spec = new LibraryAudioBookSpecification();
            var abooks = await _audioBookRepo.GetListWithSpecAsync(spec);;
            return Ok(_mapper
                .Map<IReadOnlyList<AudioBook>, IReadOnlyList<AudioBookInLibraryDto>>(abooks));
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
            var spec = new LibraryAudioBookSpecification(id);
            return await _audioBookRepo.GetEntityWithSpec(spec);
        }
    }
}
