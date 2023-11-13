using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Entities.Identity;
using System.Text.Json;

namespace Re_ABP_Backend.Data.DB
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDBContext context)
        {
            if (!context.Genre.Any())
            {
                var genressData = File.ReadAllText("Data/DB/SeedDB/genres.json");
                var genres = JsonSerializer.Deserialize<List<Genre>>(genressData);
                context.Genre.AddRange(genres);
                await context.SaveChangesAsync();
            }

            if (!context.BookLanguage.Any())
            {
                var booklanguageData = File.ReadAllText("Data/DB/SeedDB/bookLanguages.json");
                var booklanguage = JsonSerializer.Deserialize<List<BookLanguage>>(booklanguageData);
                context.BookLanguage.AddRange(booklanguage);
                await context.SaveChangesAsync();
            }

            if (!context.Narrator.Any())
            {
                var narratorsData = File.ReadAllText("Data/DB/SeedDB/narrators.json");
                var narrators = JsonSerializer.Deserialize<List<Narrator>>(narratorsData);
                context.Narrator.AddRange(narrators);
                await context.SaveChangesAsync();
            }

            if (!context.BookSeries.Any())
            {
                var bookseriesData = File.ReadAllText("Data/DB/SeedDB/bookseries.json");
                var bookseries = JsonSerializer.Deserialize<List<BookSeries>>(bookseriesData);
                context.BookSeries.AddRange(bookseries);
                await context.SaveChangesAsync();
            }

            if (!context.Author.Any())
            {
                var authorsData = File.ReadAllText("Data/DB/SeedDB/authors.json");
                var authors = JsonSerializer.Deserialize<List<Author>>(authorsData);
                context.Author.AddRange(authors);
                await context.SaveChangesAsync();
            }

            if (!context.AudioBook.Any())
            {
                var audioBooksData = File.ReadAllText("Data/DB/SeedDB/audiobooks.json");
                var audiobooks = JsonSerializer.Deserialize<List<AudioBook>>(audioBooksData);
                context.AudioBook.AddRange(audiobooks);
                await context.SaveChangesAsync();
            }

            if (!context.AudioBookAuthor.Any())
            {
                var audioBooksAuthorData = File.ReadAllText("Data/DB/SeedDB/audiobooks-authors.json");
                var audioBooksAthors = JsonSerializer.Deserialize<List<AudioBookAuthor>>(audioBooksAuthorData);
                context.AudioBookAuthor.AddRange(audioBooksAthors);
                await context.SaveChangesAsync();
            }

            if (!context.AudioBookGenre.Any())
            {
                var audioBooksGenresDat = File.ReadAllText("Data/DB/SeedDB/audiobooks-genres.json");
                var audioBookGenre = JsonSerializer.Deserialize<List<AudioBookGenre>>(audioBooksGenresDat);
                context.AudioBookGenre.AddRange(audioBookGenre);
                await context.SaveChangesAsync();
            }

            if (!context.BookAudioFile.Any())
            {
                var audioFilesData = File.ReadAllText("Data/DB/SeedDB/audioFiles.json");
                var audioFiles = JsonSerializer.Deserialize<List<BookAudioFile>>(audioFilesData);
                context.BookAudioFile.AddRange(audioFiles);
                await context.SaveChangesAsync();
            }

            if (!context.AudioBookAudioFile.Any())
            {
                var audioBooksAudioFilesData = File.ReadAllText("Data/DB/SeedDB/audiobook-audiofile.json");
                var audioBooksAudioFiles = JsonSerializer.Deserialize<List<AudioBookAudioFile>>(audioBooksAudioFilesData);
                context.AudioBookAudioFile.AddRange(audioBooksAudioFiles);
                await context.SaveChangesAsync();
            }

            if (!context.BookSelection.Any())
            {
                var bookSelection = File.ReadAllText("Data/DB/SeedDB/selections.json");
                var bookSelections = JsonSerializer.Deserialize<List<BookSelection>>(bookSelection);
                context.BookSelection.AddRange(bookSelections);
                await context.SaveChangesAsync();
            }

            if (!context.AudioBookSelection.Any())
            {
                var bookSelection_audiobook = File.ReadAllText("Data/DB/SeedDB/bookselections_books.json");
                var bookSelections_audiobook = JsonSerializer.Deserialize<List<AudioBookSelection>>(bookSelection_audiobook);
                context.AudioBookSelection.AddRange(bookSelections_audiobook);
                await context.SaveChangesAsync();
            }

            if (!context.LibraryStatus.Any())
            {
                var libraryStatus = File.ReadAllText("Data/DB/SeedDB/libraryStatus.json");
                var libraryStatuses = JsonSerializer.Deserialize<List<LibraryStatus>>(libraryStatus);
                context.LibraryStatus.AddRange(libraryStatuses);
                await context.SaveChangesAsync();
            }

            if (!context.Role.Any())
            {
                var role = File.ReadAllText("Data/DB/SeedDB/roles.json");
                var roles = JsonSerializer.Deserialize<List<Role>>(role);
                context.Role.AddRange(roles);
                await context.SaveChangesAsync();
            }

        }
    }
}
