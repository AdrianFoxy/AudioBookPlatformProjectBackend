using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Specification.SpecClasses
{
    public class LibraryAudioBookSpecification : BaseSpecification<AudioBook>
    {
        public LibraryAudioBookSpecification(string sort)
        {
            AddInclude(x => x.Author);
            AddInclude(x => x.Genre);
            AddInclude(x => x.Narrator);
            AddInclude(x => x.BookSeries);
            AddInclude(x => x.BookLanguage);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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
