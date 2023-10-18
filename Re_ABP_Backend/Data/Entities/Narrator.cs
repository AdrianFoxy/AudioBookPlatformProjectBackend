using Re_ABP_Backend.Data.Entities.Base;

namespace Re_ABP_Backend.Data.Entities
{
    public class Narrator : BaseEntity
    {
        public string Name { get; set; }
        public string MediaUrl { get; set; }
    }
}
