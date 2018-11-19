using BlogHostCore.DomainModels;
using BlogHostCore.Interfaces;
using BlogHostCore.Interfaces.Repository;
using BlogHostCore.Interfaces.Services;

namespace Infrastructure.Services
{
    public class AuthenticationRegisterService : IAuthenticationRegisterService
    {
        public IUserRepository users;

        public AuthenticationRegisterService(IUserRepository users) => this.users = users;

        public bool CreateNewUser(string nickname, string email, string password, IHashable hashManager, string role = "User")
        {
            var user = users.GetByName(nickname);
            if (user == null)
            {
                hashManager.Password = password;
                users.Add(new UserInfo()
                {
                    Email = email,
                    Password = hashManager.GetHash(),
                    Nickname = nickname
                }, role);
                return true;
            }
            return false;
        }


        public bool UserAuthentication(string email, string password, IHashable hashManager)
        {
            hashManager.Password = password;
            var user = users.GetByEmail(email);
            return user != null && hashManager.VerifyPassword(user.Password);
        }
    }
}
