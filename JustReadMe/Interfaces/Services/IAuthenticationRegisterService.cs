using JustReadMe.ViewModels;

namespace JustReadMe.Interfaces.Services
{
    public interface IAuthenticationRegisterService
    {
        bool CreateNewUser(RegisterModel model, IHashable hashManager);
        bool UserAuthentication(LoginModel model, IHashable hashManager);
    }
}
