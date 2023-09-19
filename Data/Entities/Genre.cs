namespace ABP_Backend.Data.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public List<AudioBook> AudioBooks { get; set; }
    }
}
