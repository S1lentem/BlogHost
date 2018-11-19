namespace BlogHostCore.Interfaces.Services
{
    public interface IAuthenticationRegisterService
    {
        bool CreateNewUser(string nickname, string email, string password, IHashable hashManager, string role = "User");
        bool UserAuthentication(string email, string password, IHashable hashManager);
    }
}
