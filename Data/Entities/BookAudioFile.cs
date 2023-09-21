using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABP_Backend.Data.Entities
{
    public class BookAudioFile : BaseEntity
    {
        public string Name { get; set; }
        public string AudioFileUrl { get; set; }
        [Column(TypeName = "TIME")]
        public TimeSpan Duration { get; set; }
        public List<AudioBook> AudioBook { get; set; }
        public List<AudioBookAudioFile> AudioBookBookAudioFile { get; set; }

    }
}