using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Specification.Params;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.AdminBookSeriesSpec
{
    public class BookSeriesCountSpecification : BaseSpecification<BookSeries>
    {
        public BookSeriesCountSpecification(PagAndSearchParams abParams)
            : base(x => (string.IsNullOrEmpty(abParams.Search) || x.Name.ToLower().Contains(abParams.Search) || x.EnName.ToLower().Contains(abParams.Search)))
        {
        }

    }
}
