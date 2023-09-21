namespace ABP_Backend.Data.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public List<AudioBookGenre> AudioBookGenre { get; set; }
        public List<AudioBook> AudioBook { get; set; }

    }
}
