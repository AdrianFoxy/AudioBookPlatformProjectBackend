using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Re_ABP_Backend.Data.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Re_ABP_Backend.Data.Entities
{
    public class BookAudioFile : BaseEntity
    {
        public string Name { get; set; }
        public string AudioFileUrl { get; set; }
        public int Duration { get; set; }
        public int PlaybackQueue { get; set; }

        [JsonIgnore]

        public List<AudioBook> AudioBook { get; set; }
        [JsonIgnore]

        public List<AudioBookAudioFile> AudioBookBookAudioFile { get; set; }

    }
}