using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Specification.Params;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.AdminAudioBook
{
    public class AudioBookSpecification : BaseSpecification<AudioBook>
    {
        public AudioBookSpecification(PagAndSearchParams abParams)
           : base(x =>
                (string.IsNullOrEmpty(abParams.Search) || x.Name.ToLower().Contains(abParams.Search)))
        {
            AddInclude(x => x.Author);
            AddInclude(x => x.Genre);
            AddInclude(x => x.Narrator);
            AddInclude(x => x.BookSeries);
            AddInclude(x => x.BookLanguage);

            ApplyPaging(abParams.PageSize * (abParams.PageIndex - 1),
                abParams.PageSize);
        }
    }
}
