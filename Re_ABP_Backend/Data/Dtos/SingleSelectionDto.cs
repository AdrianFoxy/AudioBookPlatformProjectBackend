namespace Re_ABP_Backend.Data.Dtos
{
    public class SingleSelectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }

        public string Description { get; set; }
        public string EnDescription { get; set; }

        public string ImageUrl { get; set; }
        public int ViewCount { get; set; }
    }
}
