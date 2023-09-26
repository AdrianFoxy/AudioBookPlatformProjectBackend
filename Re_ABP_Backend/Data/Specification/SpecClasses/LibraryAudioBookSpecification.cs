using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Specification.SpecClasses
{
    public class LibraryAudioBookSpecification : BaseSpecification<AudioBook>
    {
        public LibraryAudioBookSpecification(ABSpecParams abParams)
            : base(x =>
                (string.IsNullOrEmpty(abParams.Search) || x.Name.ToLower().Contains(abParams.Search)) &&
                (abParams.AuthorIds == null || abParams.AuthorIds.Count == 0 || x.Author.Any(x => abParams.AuthorIds.Contains(x.Id))) &&
                (abParams.GenreIds == null || abParams.GenreIds.Count == 0 || x.Genre.Any(x => abParams.GenreIds.Contains(x.Id)))
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
