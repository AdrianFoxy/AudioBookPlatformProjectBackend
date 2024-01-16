using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Specification.Params;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.AdminNarratorSpec
{
    public class NarratorCountSpecification : BaseSpecification<Narrator>
    {
        public NarratorCountSpecification(PagAndSearchParams abParams)
            : base(x => (string.IsNullOrEmpty(abParams.Search) || x.Name.ToLower().Contains(abParams.Search)))
        {
        }
    }
}
