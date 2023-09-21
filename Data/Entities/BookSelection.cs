namespace ABP_Backend.Data.Entities
{
    public class BookSelection : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<AudioBook> AudioBook { get; set; }
        public List<AudioBookSelection> AudioBookSelection { get; set; }


    }
}