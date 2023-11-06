using Newtonsoft.Json;
using Re_ABP_Backend.Data.Entities.Base;
using Re_ABP_Backend.Data.Entities.Identity;

namespace Re_ABP_Backend.Data.Entities
{
    public class Review : BaseEntity
    {
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public int AudioBookId { get; set; }
        [JsonIgnore]
        public AudioBook AudioBook { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
