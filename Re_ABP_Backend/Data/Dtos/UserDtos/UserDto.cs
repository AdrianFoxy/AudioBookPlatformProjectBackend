namespace Re_ABP_Backend.Data.Dtos.UserDtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
