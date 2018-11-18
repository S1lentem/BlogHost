namespace BlogHostCore.DomainModels
{
    public class UserInfo
    {
        public int Id { get; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}
