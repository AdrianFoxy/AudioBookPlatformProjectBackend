using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.ReviewSpec
{
    public class ReviewSpecification : BaseSpecification<Review>
    {
        public ReviewSpecification(ABOfSomethingParams abParams) :
            base(x => (x.AudioBookId == abParams.Id))
        {
            AddInclude(x => x.User);
            ApplyPaging(abParams.PageSize * (abParams.PageIndex - 1),
                    abParams.PageSize);
        }
    }
}
