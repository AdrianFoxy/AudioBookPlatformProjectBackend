namespace Re_ABP_Backend.Data.Entities
{
    public class User
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set;}
    }
}
