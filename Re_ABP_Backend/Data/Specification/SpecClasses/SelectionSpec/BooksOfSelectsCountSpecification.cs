using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.SelectionSpec
{
    public class BooksOfSelectsCountSpecification : BaseSpecification<AudioBook>
    {
        public BooksOfSelectsCountSpecification(ABOfSomethingParams abParams) :
                     base(x => (x.BookSelection.Any(a => a.Id == abParams.Id)))
        {
            AddInclude(x => x.Author);
        }
    }
}
