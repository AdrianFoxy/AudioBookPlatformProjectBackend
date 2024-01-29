using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Specification.Params;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.AdminAuthor
{
    public class AuthorSpecification : BaseSpecification<Author>
    {
        public AuthorSpecification(PagAndSearchParams abParams)
            : base(x => (string.IsNullOrEmpty(abParams.Search) || x.Name.ToLower().Contains(abParams.Search) || x.EnName.ToLower().Contains(abParams.Search)))
        {
            ApplyPaging(abParams.PageSize * (abParams.PageIndex - 1),
                    abParams.PageSize);
        }

    }
}
