using System.Text.Json.Serialization;
using Re_ABP_Backend.Data.Entities.Base;

namespace Re_ABP_Backend.Data.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public string EnName { get; set; }

        [JsonIgnore]

        public List<AudioBookGenre> AudioBookGenre { get; set; }
        [JsonIgnore]

        public List<AudioBook> AudioBook { get; set; }

    }
}
