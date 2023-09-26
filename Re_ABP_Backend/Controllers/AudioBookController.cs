﻿using Re_ABP_Backend.Data.Dtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Data.Specification.SpecClasses;
using Re_ABP_Backend.Errors;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Re_ABP_Backend.Data.Specification;
using Re_ABP_Backend.Data.Helpers;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioBookController : ControllerBase
    {
        private readonly IGenericRepository<AudioBook> _audioBookRepo;
        private readonly IGenericRepository<Genre> _genreRepo;
        private readonly IGenericRepository<Author> _authorRepo;
        private readonly IMapper _mapper;

        public AudioBookController(IGenericRepository<AudioBook> audioBookRepo,
                                   IGenericRepository<Genre> genreRepo,
                                   IGenericRepository<Author> authorRepo,
                                   IMapper mapper)
        {
            _audioBookRepo = audioBookRepo;
            _genreRepo = genreRepo;
            _authorRepo = authorRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<AudioBookInLibraryDto>>> GetBooksAsync(
            [FromQuery] ABSpecParams abParams)
        {
            var spec = new LibraryAudioBookSpecification(abParams);
            var countSpec = new LibraryAudioBookForCountSpecification(abParams);

            var totalItems = await _audioBookRepo.CountAsync(countSpec);

            var abooks = await _audioBookRepo.GetListWithSpecAsync(spec);

            var data = _mapper
                .Map<IReadOnlyList<AudioBook>, IReadOnlyList<AudioBookInLibraryDto>>(abooks);
            return Ok(new Pagination<AudioBookInLibraryDto>(abParams.PageSize,
                abParams.PageSize, totalItems, data));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AudioBook>> GetBookAsync(int id)
        {
            var spec = new LibraryAudioBookSpecification(id);
            var aidiobook = await _audioBookRepo.GetEntityWithSpec(spec);

            if (aidiobook == null) 
            {
                Log.Information("Request to get audiobook by id failed, book with id {Id} does not exists.", id);
                return NotFound(new ApiResponse(404));
            }
            return Ok(aidiobook);
        }

        [HttpGet("get-authors")]
        public async Task<ActionResult<Author>> GetAuthorsAsycn()
        {
            return Ok(await _authorRepo.GetListAllAsync());
        }
    }
}
