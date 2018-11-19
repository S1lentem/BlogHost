using BlogHostCore.DomainModels;
using System.Collections.Generic;

namespace BlogHostCore.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<UserInfo>
    {
        void Add(UserInfo model, string role = "user");
        UserInfo GetByName(string name);
        UserInfo GetByEmail(string email);
        UserInfo GetFullInfoByEmail(string email);
        void SetRole(string userName, string role);
        UserInfo GetFullInfoByName(string name);
        IEnumerable<UserInfo> GetFullInfoForAllUsers();
    }
}
