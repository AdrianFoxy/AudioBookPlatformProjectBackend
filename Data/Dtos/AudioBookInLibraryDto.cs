using ABP_Backend.Data.Entities;
using System.Text.Json.Serialization;

namespace ABP_Backend.Data.Dtos
{
    public class AudioBookInLibraryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public float Rating { get; set; }
        public TimeSpan BookDuration { get; set; }
        public List<Author> Author { get; set; }
    }
}
