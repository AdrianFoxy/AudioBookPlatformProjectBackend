using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.AuthorSpec
{
    public class AuthorBooksCountSpecification : BaseSpecification<AudioBook>
    {
        public AuthorBooksCountSpecification(AuthorBooksParam abParams):
             base(x => (x.Author.Any(a => a.Id == abParams.AuthorId)))
        {
        }
    }
}
