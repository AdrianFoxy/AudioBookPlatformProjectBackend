using System.Text.Json.Serialization;

namespace ABP_Backend.Data.Entities
{
    public class BookSelection : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [JsonIgnore]

        public List<AudioBook> AudioBook { get; set; }
        [JsonIgnore]

        public List<AudioBookSelection> AudioBookSelection { get; set; }


    }
}