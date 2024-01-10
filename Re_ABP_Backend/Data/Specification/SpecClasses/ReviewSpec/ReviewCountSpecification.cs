using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Specification.Params;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.ReviewSpec
{
    public class ReviewCountSpecification : BaseSpecification<Review>
    {
        public ReviewCountSpecification(PaginationWithIdParams abParams) :
                    base(x => (x.AudioBookId == abParams.Id))
        {
        }

    }
}
