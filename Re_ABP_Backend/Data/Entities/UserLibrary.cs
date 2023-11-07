using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Re_ABP_Backend.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Re_ABP_Backend.Data.Entities
{
    public class UserLibrary
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int AudioBookId { get; set; }
        public AudioBook AudioBook { get; set; }

        public int LibraryStatusId { get; set; }
        public LibraryStatus LibraryStatus { get; set; }
        [Column(TypeName = "DateTime")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime UpdatedAt { get; set; }
    }
}
