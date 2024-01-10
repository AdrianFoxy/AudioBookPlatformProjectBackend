using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Specification.Params;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.AuthorSpec
{
    public class AuthorBooksCountSpecification : BaseSpecification<AudioBook>
    {
        public AuthorBooksCountSpecification(PaginationWithIdParams abParams):
             base(x => (x.Author.Any(a => a.Id == abParams.Id)))
        {
        }
    }
}
