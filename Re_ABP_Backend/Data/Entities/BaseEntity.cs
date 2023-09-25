using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Re_ABP_Backend.Data.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [Column(TypeName = "DateTime")]

        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "DateTime")]

        public DateTime UpdatedAt { get; set; }
    }
}
