using System.ComponentModel.DataAnnotations.Schema;

namespace ABP_Backend.Data.Entities
{
    public class AudioBook
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Name { get; set; }
    }
}
