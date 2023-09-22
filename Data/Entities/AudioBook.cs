using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ABP_Backend.Data.Entities
{
    public class AudioBook : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public float Rating { get; set; }
        public TimeSpan BookDuration { get; set; }

        // Other tables, relationships, 1:1, 1:n, n:n
        [JsonIgnore]
        public List<AudioBookGenre> AudioBookGenre { get; set; }
        public List<Genre> Genre { get; set; }

        // Did I forget why I`m using public List<AudioBookAuthor> AudioBookAuthor and public List<Author> Author at the same time? 
        // Friendly remind: https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many, Many-to-many with navigations to join entity
        [JsonIgnore]
        public List<AudioBookAuthor> AudioBookAuthor { get; set; }
        public List<Author> Author { get; set; }
        [JsonIgnore]
        public List<AudioBookSelection> AudioBookSelection { get; set; }
        public List<BookSelection> BookSelection { get; set; }
        [JsonIgnore]

        public List<AudioBookAudioFile> AudioBookAudioFile { get; set; }
        public List<BookAudioFile> BookAudioFile { get; set; }
        public BookLanguage BookLanguage { get; set; }
        public int BookLanguageId { get; set; }
        public Narrator Narrator { get; set; }
        public int NarratorId { get; set; }
        public BookSeries BookSeries { get; set; }
        public int BookSeriesId { get; set; }
    }
}
