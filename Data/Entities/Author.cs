namespace ABP_Backend.Data.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<AudioBookAuthor> AudioBookAuthor { get; set; }
        public List<AudioBook> AudioBook { get; set; }

    }
}