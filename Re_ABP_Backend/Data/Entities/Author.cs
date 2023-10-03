using System.Text.Json.Serialization;

namespace Re_ABP_Backend.Data.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string EnName { get; set; }
        public string Description { get; set; }
        public string EnDescription { get; set; }
        public string ImageUrl { get; set; }
        [JsonIgnore]
        public List<AudioBookAuthor> AudioBookAuthor { get; set; }
        [JsonIgnore]
        public List<AudioBook> AudioBook { get; set; }

    }
}