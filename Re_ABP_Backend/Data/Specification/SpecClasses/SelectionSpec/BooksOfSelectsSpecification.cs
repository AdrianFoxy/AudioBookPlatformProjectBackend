using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Specification.Params;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.SelectionSpec
{
    public class BooksOfSelectsSpecification : BaseSpecification<AudioBook>
    {
        public BooksOfSelectsSpecification(PaginationWithIdParams abParams) :
               base(x => (x.BookSelection.Any(a => a.Id == abParams.Id)))
        {
            AddInclude(x => x.Author);
            ApplyPaging(abParams.PageSize * (abParams.PageIndex - 1),
                    abParams.PageSize);
        }
    }
}
