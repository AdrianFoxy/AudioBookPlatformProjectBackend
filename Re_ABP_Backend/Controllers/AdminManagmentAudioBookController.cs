﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.AudioBooksDtos;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.AudioBooksDtos.AudioFiles;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.AuthorDtos;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Entities.Picture;
using Re_ABP_Backend.Data.Helpers;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Data.Specification.Params;
using Re_ABP_Backend.Data.Specification.SpecClasses.AdminAudioBook;
using Re_ABP_Backend.Data.Specification.SpecClasses.AudioBooks;
using Re_ABP_Backend.Errors;
using Re_ABP_Backend.Resources;
using Serilog;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminManagmentAudioBookController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPictureService _pictureService;
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;

        public AdminManagmentAudioBookController(IMapper mapper, IUnitOfWork unitOfWork, IPictureService pictureService, IStringLocalizer<SharedResource> sharedResourceLocalizer)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _pictureService = pictureService;
            _sharedResourceLocalizer = sharedResourceLocalizer;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<AudioBookInListDto>>> GetAuthorsList([FromQuery] PagAndSearchParams pagAndSearchParams)
        {
            var spec = new AudioBookSpecification(pagAndSearchParams);
            var countSpec = new AudioBookCountSpecification(pagAndSearchParams);

            var totalItems = await _unitOfWork.Repository<AudioBook>().CountAsync(countSpec);
            var items = await _unitOfWork.Repository<AudioBook>().GetListWithSpecAsync(spec);

            var data = _mapper
                .Map<IReadOnlyList<AudioBook>, IReadOnlyList<AudioBookInListDto>>(items);

            return Ok(new Pagination<AudioBookInListDto>(pagAndSearchParams.PageIndex, pagAndSearchParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AudioBookDetailsDto>> GetBookAsync(int id)
        {
            var spec = new LibraryAudioBookSpecification(id);
            var audioBook = await _unitOfWork.Repository<AudioBook>().GetEntityWithSpec(spec);

            if (audioBook == null)
            {
                Log.Error("Request to get audiobook by id failed, book with id {Id} does not exist.", id);
                return NotFound(new ApiResponse(404));
            }

            audioBook.BookAudioFile = audioBook.BookAudioFile.OrderBy(baf => baf.PlaybackQueue).ToList();
            var data = _mapper.Map<AudioBook, AudioBookDetailsDto>(audioBook);

            return Ok(data);
        }


        // Doesn`t work correctly in Swagger, so tests in Postman
        // Because I can't send a list of objects to form-data
        // only format like:
        // AudioFileUrls[0].Name, AudioFileUrls[1].Name, etc
        [HttpPost]
        public async Task<ActionResult<AudioBook>> AddAudioBook([FromForm] AddAudioBookDto addAudioBookDto)
        {
            try
            {
                if (addAudioBookDto.Picture == null || addAudioBookDto.Picture.Length == 0)
                {
                    return BadRequest("The picture field is required.");
                }

                var picture = await _pictureService.SaveToDiskAsync(addAudioBookDto.Picture, PictureType.books);

                if (picture == null)
                {
                    ModelState.AddModelError(nameof(addAudioBookDto.Picture), "Error during saving picture.");
                    return BadRequest(ModelState);
                }

                var item = _mapper.Map<AddAudioBookDto, AudioBook>(addAudioBookDto);

                item.PictureUrl = picture.PictureUrl;

                var audioBookGenres = addAudioBookDto.GenresIds.Select(genreId => new AudioBookGenre { GenreId = genreId }).ToList();
                var audioBookAuthors = addAudioBookDto.AuthorsIds.Select(authorId => new AudioBookAuthor { AuthorId = authorId }).ToList();
                var audioBookSelection = addAudioBookDto.BookSelectionsIds.Select(selectionId => new AudioBookSelection { BookSelectionId = selectionId }).ToList();

                item.AudioBookAuthor = audioBookAuthors;
                item.AudioBookGenre = audioBookGenres;
                item.AudioBookSelection = audioBookSelection;

                item.BookAudioFile = new List<BookAudioFile>(); 

                if (addAudioBookDto.AudioFileUrls != null && addAudioBookDto.AudioFileUrls.Any())
                {
                    foreach (var audioFileDto in addAudioBookDto.AudioFileUrls)
                    {
                        var audioFile = new BookAudioFile
                        {
                            Name = audioFileDto.Name,
                            AudioFileUrl = audioFileDto.AudioFileUrl,
                            Duration = audioFileDto.Duration,
                            PlaybackQueue = audioFileDto.PlaybackQueue
                        };

                        // Connect with audiobook
                        audioFile.AudioBook = item;
                        item.BookAudioFile.Add(audioFile);
                    }
                }

                _unitOfWork.Repository<AudioBook>().Add(item);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem creating new audiobook.");
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemCreatingAuthor")));
                }

                return Ok(item);
            }
            catch (DbUpdateException ex) when (SQLExceptionHandler.IsUniqueConstraintViolationException(ex))
            {
                Log.Error("Audiobook with this name already exists.");
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("UniqAuthor")));
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAudioBook(int id)
        {
            try
            {
                var item = await _unitOfWork.Repository<AudioBook>().GetByIdAsync(id);

                if (item == null)
                {
                    Log.Error("Error deleting audiobook. Audiobook id: {id} not found", id);
                    return NotFound();
                }

                _unitOfWork.Repository<AudioBook>().Delete(item);
                _pictureService.DeleteFromDisk(Path.GetFileName(item.PictureUrl), PictureType.books);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    Log.Error("Problem deleting audiobook. Audiobook id: {id}", id);
                    return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemDeletingAuthor")));
                }

                return Ok();
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "Error deleting audiobook. Audiobook id: {id}", id);
                return BadRequest(new ApiResponse(400, _sharedResourceLocalizer.GetString("ProblemDeletingAuthorAssociated")));
            }
        }

    }
}
