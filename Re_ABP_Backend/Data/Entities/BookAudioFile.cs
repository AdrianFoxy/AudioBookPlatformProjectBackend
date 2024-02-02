using Re_ABP_Backend.Data.Entities.Base;
using System.Text.Json.Serialization;

namespace Re_ABP_Backend.Data.Entities
{
    public class BookAudioFile : BaseEntity
    {
        public string Name { get; set; }
        public string AudioFileUrl { get; set; }
        public int Duration { get; set; }
        public int PlaybackQueue { get; set; }
        public int AudioBookId { get; set; }
        public AudioBook AudioBook { get; set; }
    }
}