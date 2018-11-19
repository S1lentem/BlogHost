using BlogHostCore.DomainModels;
using Infrastructure.Models;

namespace Infrastructure.Services.Map.Sql
{
    public class UserSqlMapper
    {
        public UserInfo GetDomain(UserModel model)
        {
            return new UserInfo()
            {
                Id = model.Id,
                Email = model.Email,
                Nickname = model.Nickname,
                Password = model.Password
            };
        }
    }
}
