namespace Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.AuthorDtos
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public string Description { get; set; }
        public string EnDescription { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
