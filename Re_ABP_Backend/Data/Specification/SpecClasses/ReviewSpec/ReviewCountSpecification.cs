using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.ReviewSpec
{
    public class ReviewCountSpecification : BaseSpecification<Review>
    {
        public ReviewCountSpecification(ABOfSomethingParams abParams) :
                    base(x => (x.AudioBookId == abParams.Id))
        {
        }

    }
}
