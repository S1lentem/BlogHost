using JustReadMe.ViewModels;
using System.Threading.Tasks;

namespace JustReadMe.Interfaces.Services
{
    public interface IAuthenticationRegisterService
    {
        Task<bool> CreateNewUser(RegisterModel model, IHashable hashManager);
        Task<bool> UserAuthentication(LoginModel model, IHashable hashManager);
    }
}
