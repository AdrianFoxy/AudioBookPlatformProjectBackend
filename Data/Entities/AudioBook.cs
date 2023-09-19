using System.ComponentModel.DataAnnotations.Schema;

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
        public List<Genre> Genre { get; set; }
        public List<Author> Author { get; set; }
        public List<BookSelection> BookSelection { get; set; }
        public List<BookAudioFile> BookAudioFile { get; set; }
        public BookLanguage BookLanguage { get; set; }
        public int BookLanguageId { get; set; }
        public Narrator Narrator { get; set; }
        public int NarratorId { get; set; }
        public BookSeries BookSeries { get; set; }
        public int BookSeriesId { get; set; }
    }
}
