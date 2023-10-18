using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Re_ABP_Backend.Data.Entities.Identity
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "DateTime")]

        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "DateTime")]

        public DateTime UpdatedAt { get; set; }
    }
}
