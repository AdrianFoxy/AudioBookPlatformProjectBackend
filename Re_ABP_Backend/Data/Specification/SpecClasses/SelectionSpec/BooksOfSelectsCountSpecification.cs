using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Specification.Params;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.SelectionSpec
{
    public class BooksOfSelectsCountSpecification : BaseSpecification<AudioBook>
    {
        public BooksOfSelectsCountSpecification(PaginationWithIdParams abParams) :
                     base(x => (x.BookSelection.Any(a => a.Id == abParams.Id)))
        {
            AddInclude(x => x.Author);
        }
    }
}
