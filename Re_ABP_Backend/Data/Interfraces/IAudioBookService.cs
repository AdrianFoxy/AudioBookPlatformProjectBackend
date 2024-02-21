using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.AudioBooksDtos;
using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Interfraces
{
    public interface IAudioBookService
    {
        Task<bool> IncreaseViewCountAsync(int audioBookId);
        void UpdateGenresAndAuthors(AudioBook existingAudioBook, IEnumerable<int> genreIds, IEnumerable<int> authorIds);
        void UpdateAudioFiles(AudioBook existingAudioBook, string audioFilesJson);
        void AddAudioFilesToAudioBook(AddAudioBookDto addAudioBookDto, AudioBook item);
        public void DeleteAudioFiles(AudioBook audioBook, IEnumerable<int> audioFilesToDelete);
    }
}
