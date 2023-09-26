using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Specification.SpecClasses
{
    public class LibraryAudioBookForCountSpecification : BaseSpecification<AudioBook>
    {
        public LibraryAudioBookForCountSpecification(ABSpecParams abParams)
            : base(x =>
                (string.IsNullOrEmpty(abParams.Search) || x.Name.ToLower().Contains(abParams.Search)) &&
                (abParams.AuthorIds == null || abParams.AuthorIds.Count == 0 || x.Author.Any(x => abParams.AuthorIds.Contains(x.Id))) &&
                (abParams.GenreIds == null || abParams.GenreIds.Count == 0 || x.Genre.Any(x => abParams.GenreIds.Contains(x.Id)))
            )
        {
        }
    }
}
