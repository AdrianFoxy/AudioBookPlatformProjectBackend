﻿using Re_ABP_Backend.Data.Dtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Data.Specification.SpecClasses;
using Re_ABP_Backend.Errors;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Re_ABP_Backend.Controllers
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

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AudioBookInLibraryDto>>> GetBooks()
        {
            var spec = new LibraryAudioBookSpecification();
            var abooks = await _audioBookRepo.GetListWithSpecAsync(spec);;
            return Ok(_mapper
                .Map<IReadOnlyList<AudioBook>, IReadOnlyList<AudioBookInLibraryDto>>(abooks));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AudioBook>> GetBook(int id)
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
    }
}
