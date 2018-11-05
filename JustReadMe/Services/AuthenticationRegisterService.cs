using System.Threading.Tasks;
using JustReadMe.Interfaces;
using JustReadMe.Interfaces.Repository;
using JustReadMe.Interfaces.Services;
using JustReadMe.Models;
using JustReadMe.ViewModels;

namespace JustReadMe.Services
{
    public class AuthenticationRegisterService : IAuthenticationRegisterService
    {
        public IUserRepository users;

        public AuthenticationRegisterService(IUserRepository users) => this.users = users;

        public bool CreateNewUser(RegisterModel model, IHashable hashManager)
        {
            var user = users.GetByEmail(model.Email);
            if (user == null)
            {
                hashManager.Password = model.Password;
                users.Add(new UserModel()
                {
                    Email = model.Email,
                    Password = hashManager.GetHash(),
                    Nickname = model.Nickname
                });
                return true;
            }
            return false;
        }

        public bool UserAuthentication(LoginModel model, IHashable hashManager)
        {
            hashManager.Password = model.Passwords;
            var user = users.GetByEmail(model.Email);
            return user != null && hashManager.VerifyPassword(user.Password);
        }
    }
}
