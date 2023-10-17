using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.AudioBookSpec
{
    public class AuthorBooksSpecification : BaseSpecification<AudioBook>
    {
        public AuthorBooksSpecification (AuthorBooksParam abParams):
            base(x => (x.Author.Any(a => a.Id == abParams.AuthorId)))
        {
            AddInclude(x => x.Author);
            ApplyPaging(abParams.PageSize * (abParams.PageIndex - 1),
                    abParams.PageSize);
        }
    }
}
