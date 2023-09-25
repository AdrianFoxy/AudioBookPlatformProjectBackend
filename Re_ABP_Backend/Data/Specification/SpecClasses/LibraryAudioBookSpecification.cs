using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Specification.SpecClasses
{
    public class LibraryAudioBookSpecification : BaseSpecification<AudioBook>
    {
        public LibraryAudioBookSpecification()
        {
            AddInclude(x => x.Author);
            AddInclude(x => x.Genre);
            AddInclude(x => x.Narrator);
            AddInclude(x => x.BookSeries);
            AddInclude(x => x.BookLanguage);
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
