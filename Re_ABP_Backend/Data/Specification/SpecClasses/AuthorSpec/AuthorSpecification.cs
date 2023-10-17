
using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.AuthorSpec
{
    public class AuthorSpecification : BaseSpecification<Author>
    {
        public AuthorSpecification(int authorId) : 
           base (x => x.Id == authorId)
        { }

    }
}
