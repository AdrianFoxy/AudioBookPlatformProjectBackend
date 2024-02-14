using Newtonsoft.Json;
using Re_ABP_Backend.Data.DB;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.AudioBooksDtos;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.AudioBooksDtos.AudioFiles;
using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Interfraces;
using Serilog;

namespace Re_ABP_Backend.Data.Services
{
    public class AudioBookService : IAudioBookService
    {
        private readonly AppDBContext _context;
        public AudioBookService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> IncreaseViewCountAsync(int audioBookId)
        {
            var audioBook = await _context.AudioBook.FindAsync(audioBookId);

            if (audioBook == null)
            {
                Log.Error("IncreaseViewCountAsync METHOD: Request to get audiobook by id failed, book with id {audioBookId} does not exists.", audioBookId);
                return false;
            }

            audioBook.ViewCount++;
            await _context.SaveChangesAsync();

            return true;
        }

        public void AddAudioFilesToAudioBook(AddAudioBookDto addAudioBookDto, AudioBook item)
        {
            if (!string.IsNullOrEmpty(addAudioBookDto.AudioFiles))
            {
                // Parse and deserialize the JSON string into an AddAudioFile[] object
                var audioFiles = JsonConvert.DeserializeObject<AddAudioFile[]>(addAudioBookDto.AudioFiles);

                if (audioFiles != null && audioFiles.Length > 0)
                {
                    foreach (var audioFileDto in audioFiles)
                    {
                        var audioFile = new BookAudioFile
                        {
                            Name = audioFileDto.Name,
                            AudioFileUrl = audioFileDto.AudioFileUrl,
                            Duration = audioFileDto.Duration,
                            PlaybackQueue = audioFileDto.PlaybackQueue
                        };

                        // Connect with audiobook
                        audioFile.AudioBook = item;
                        item.BookAudioFile.Add(audioFile);
                    }
                }
            }
        }

        public void UpdateAudioFiles(AudioBook existingAudioBook, string audioFilesJson)
        {
            var audioFiles = JsonConvert.DeserializeObject<AddAudioFile[]>(audioFilesJson);
            if (audioFiles == null || audioFiles.Length == 0)
                return;

            if (existingAudioBook.BookAudioFile == null)
            {
                existingAudioBook.BookAudioFile = new List<BookAudioFile>();
            }

            foreach (var updatedAudioFile in audioFiles)
            {
                var existingAudioFile = existingAudioBook.BookAudioFile.FirstOrDefault(f => f.Id != null && f.Id == updatedAudioFile.Id);
                if (existingAudioFile != null)
                {
                    existingAudioFile.Name = updatedAudioFile.Name;
                    existingAudioFile.AudioFileUrl = updatedAudioFile.AudioFileUrl;
                    existingAudioFile.Duration = updatedAudioFile.Duration;
                    existingAudioFile.PlaybackQueue = updatedAudioFile.PlaybackQueue;
                }
                else
                {
                    var newAudioFile = new BookAudioFile
                    {
                        Name = updatedAudioFile.Name,
                        AudioFileUrl = updatedAudioFile.AudioFileUrl,
                        Duration = updatedAudioFile.Duration,
                        PlaybackQueue = updatedAudioFile.PlaybackQueue
                    };
                    newAudioFile.AudioBook = existingAudioBook;
                    existingAudioBook.BookAudioFile.Add(newAudioFile);
                }
            }
        }

        public void UpdateGenresAndAuthors(AudioBook existingAudioBook, IEnumerable<int> genreIds, IEnumerable<int> authorIds)
        {
            existingAudioBook.AudioBookGenre.RemoveAll(abg => !genreIds.Contains(abg.GenreId));
            foreach (var genreId in genreIds.Where(id => !existingAudioBook.AudioBookGenre.Any(abg => abg.GenreId == id)))
            {
                existingAudioBook.AudioBookGenre.Add(new AudioBookGenre { GenreId = genreId });
            }

            existingAudioBook.AudioBookAuthor.RemoveAll(aba => !authorIds.Contains(aba.AuthorId));
            foreach (var authorId in authorIds.Where(id => !existingAudioBook.AudioBookAuthor.Any(aba => aba.AuthorId == id)))
            {
                existingAudioBook.AudioBookAuthor.Add(new AudioBookAuthor { AuthorId = authorId });
            }
        }
    }
}
