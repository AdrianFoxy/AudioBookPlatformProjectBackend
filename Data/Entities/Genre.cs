using System.Text.Json.Serialization;

namespace ABP_Backend.Data.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]

        public List<AudioBookGenre> AudioBookGenre { get; set; }
        [JsonIgnore]

        public List<AudioBook> AudioBook { get; set; }

    }
}
