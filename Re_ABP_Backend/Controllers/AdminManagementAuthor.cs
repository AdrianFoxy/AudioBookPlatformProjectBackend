using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Re_ABP_Backend.Data.Dtos.PictureDtos;
using Re_ABP_Backend.Data.Entities.Picture;
using Re_ABP_Backend.Data.Interfraces;

namespace Re_ABP_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminManagementAuthor : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPictureService _pictureService;


        public AdminManagementAuthor(IMapper mapper, IUnitOfWork unitOfWork, IPictureService pictureService)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _pictureService = pictureService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPictureTest([FromForm] PictureDto picture)
        {
            if (picture.Picture == null || picture.Picture.Length == 0)
            {
                return BadRequest("The picture field is required.");
            }

            var result = await _pictureService.SaveToDiskAsync(picture.Picture, PictureType.authors);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAuthor(string pictureName)
        {
            _pictureService.DeleteFromDisk(pictureName, PictureType.authors);
            return Ok("Deleted");
        }

    }
}
