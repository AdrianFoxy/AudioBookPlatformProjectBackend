using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.AudioBooks
{
    public class RecommedantionsSpecification : BaseSpecification<AudioBook>
    {
        public RecommedantionsSpecification()
        {
            AddInclude(x => x.Author);
        }
    }
}
