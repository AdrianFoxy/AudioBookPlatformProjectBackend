using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.AudioBooks
{
    public class LibraryAudioBookSpecification : BaseSpecification<AudioBook>
    {
        public LibraryAudioBookSpecification(ABSpecParams abParams)
            : base(x =>
                (string.IsNullOrEmpty(abParams.Search) || x.Name.ToLower().Contains(abParams.Search)) &&
                (x.Rating >= abParams.LowerRating && x.Rating <= abParams.HighRating) &&
                (abParams.LowerDuration == 0 || x.BookDuration >= abParams.LowerDuration) && (abParams.HighDuration == 0 || x.BookDuration <= abParams.HighDuration) &&

                (abParams.AuthorIds == null || abParams.AuthorIds.Count == 0 || x.Author.Any(x => abParams.AuthorIds.Contains(x.Id))) &&
                (abParams.GenreIds == null || abParams.GenreIds.Count == 0 || x.Genre.Any(x => abParams.GenreIds.Contains(x.Id))) &&
                (abParams.BookSeriesIds == null || abParams.BookSeriesIds.Count == 0 || abParams.BookSeriesIds.Contains(x.BookSeriesId)) &&
                (abParams.BookLanguageIds == null || abParams.BookLanguageIds.Count == 0 || abParams.BookLanguageIds.Contains(x.BookLanguageId)) &&
                (abParams.NarratorIds == null || abParams.NarratorIds.Count == 0 || abParams.NarratorIds.Contains(x.NarratorId)) &&

                (abParams.ExceptAuthorIds == null || abParams.ExceptAuthorIds.Count == 0 || !x.Author.Any(a => abParams.ExceptAuthorIds.Contains(a.Id))) &&
                (abParams.ExceptGenreIds == null || abParams.ExceptGenreIds.Count == 0 || !x.Genre.Any(a => abParams.ExceptGenreIds.Contains(a.Id))) &&
                (abParams.ExceptBookSeriesIds == null || !abParams.ExceptBookSeriesIds.Contains(x.BookSeriesId)) &&
                (abParams.ExceptBookLanguageIds == null || !abParams.ExceptBookLanguageIds.Contains(x.BookLanguageId)) &&
                (abParams.ExceptNarratorIds == null || !abParams.ExceptNarratorIds.Contains(x.NarratorId))
            )

        {
            AddInclude(x => x.Author);
            AddInclude(x => x.Genre);
            AddInclude(x => x.Narrator);
            AddInclude(x => x.BookSeries);
            AddInclude(x => x.BookLanguage);
            ApplyPaging(abParams.PageSize * (abParams.PageIndex - 1),
                abParams.PageSize);

            if (!string.IsNullOrEmpty(abParams.Sort))
            {
                switch (abParams.Sort)
                {
                    case "rateAsc":
                        AddOrderBy(p => p.Rating);
                        break;
                    case "rateDesc":
                        AddOrderByDescending(p => p.Rating);
                        break;
                    case "durAsc":
                        AddOrderBy(p => p.BookDuration);
                        break;
                    case "durDesc":
                        AddOrderByDescending(p => p.BookDuration);
                        break;
                    case "popularity":
                        AddOrderByDescending(p => p.ViewCount);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public LibraryAudioBookSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.Author);
            AddInclude(x => x.Genre);
            AddInclude(x => x.Narrator);
            AddInclude(x => x.BookSeries);
            AddInclude(x => x.BookLanguage);
            AddInclude(x => x.BookSelection);
            AddInclude(x => x.BookAudioFile);
        }
    }
}
