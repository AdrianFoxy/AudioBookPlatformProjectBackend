using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.AudioBooksDtos.AudioFiles;
using Re_ABP_Backend.Data.Dtos.FilteringDtos;
using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.AudioBooksDtos
{
    public class AudioBookInListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Rating { get; set; }
        public string BookDuration { get; set; }
        public List<GenreFilteringDto> Genre { get; set; }
        public List<AuthorFilteringDto> Author { get; set; }
        public List<BookAudioFileDto> BookAudioFile { get; set; }
        public BookLanguage BookLanguage { get; set; }
        public Narrator Narrator { get; set; }
        public BookSeries BookSeries { get; set; }
    }
}
