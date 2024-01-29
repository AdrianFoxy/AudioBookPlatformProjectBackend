using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Specification.Params;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.AdminBookLanguageSpec
{
    public class BookLanguageCountSpecification : BaseSpecification<BookLanguage>
    {
        public BookLanguageCountSpecification(PagAndSearchParams abParams)
           : base(x => (string.IsNullOrEmpty(abParams.Search) || x.Name.ToLower().Contains(abParams.Search) || x.EnName.ToLower().Contains(abParams.Search)))
        {
        }
    }
}
