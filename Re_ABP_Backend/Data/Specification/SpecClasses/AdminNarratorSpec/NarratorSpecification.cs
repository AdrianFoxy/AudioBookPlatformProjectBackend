using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Specification.Params;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.AdminNarratorSpec
{
    public class NarratorSpecification : BaseSpecification<Narrator>
    {
        public NarratorSpecification(PagAndSearchParams abParams)
             : base(x => (string.IsNullOrEmpty(abParams.Search) || x.Name.ToLower().Contains(abParams.Search)))
        {
            ApplyPaging(abParams.PageSize * (abParams.PageIndex - 1),
                    abParams.PageSize);
        }

    }
}
