using Re_ABP_Backend.Data.Dtos.FilteringDtos;
using Re_ABP_Backend.Data.Entities;
using System.Text.Json.Serialization;

namespace Re_ABP_Backend.Data.Dtos
{
    public class AudioBookInLibraryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public float Rating { get; set; }
        public string BookDuration { get; set; }
        public List<AuthorFilteringDto> Author { get; set; }
    }
}
