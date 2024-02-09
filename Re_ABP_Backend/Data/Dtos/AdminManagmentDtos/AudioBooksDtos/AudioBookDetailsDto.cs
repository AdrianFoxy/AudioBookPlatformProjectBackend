using Re_ABP_Backend.Data.Dtos.FilteringDtos;
using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.AudioBooksDtos
{
    public class AudioBookDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public float Rating { get; set; }
        public string BookDuration { get; set; }
        public int ViewCount { get; set; }
        public int BookMarksCount { get; set; }
        public List<GenreFilteringDto> Genre { get; set; }
        public List<AuthorFilteringDto> Author { get; set; }
        public List<BookAudioFileDto> BookAudioFile { get; set; }
        public BookLanguage BookLanguage { get; set; }
        public Narrator Narrator { get; set; }
        public BookSeries BookSeries { get; set; }
        public int OrderInSeries { get; set; }
    }
}
