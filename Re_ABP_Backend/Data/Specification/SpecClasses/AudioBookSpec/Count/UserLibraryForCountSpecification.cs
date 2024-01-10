using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Specification.Params;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.AudioBookSpec.Count
{
    public class UserLibraryForCountSpecification : BaseSpecification<AudioBook>
    {
        public UserLibraryForCountSpecification(UserLibraryParams userLibraryParams)
           : base(x =>
        userLibraryParams.StatusId == 0
            ? x.UserLibrary.Any(ul => ul.UserId == userLibraryParams.UserId)
            : x.UserLibrary.Any(ul => ul.UserId == userLibraryParams.UserId && ul.LibraryStatusId == userLibraryParams.StatusId))
        {
        }

    }
}
