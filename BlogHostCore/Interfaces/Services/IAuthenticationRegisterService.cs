namespace BlogHostCore.Interfaces.Services
{
    public interface IAuthenticationRegisterService
    {
        bool CreateNewUser(string nickname, string email, string password, IHashable hashManager);
        bool UserAuthentication(string email, string password, IHashable hashManager);
    }
}
