using System.Linq;
using Infrastructure.Models;
using BlogHostCore.DomainModels;
using BlogHostCore.Interfaces.Repository;
using Infrastructure.Services.Map.Sql;

namespace Infrastructure.Storages.Sql
{
    public class UserRepository : IUserRepository
    {
        private BloghostContext context;
        private UserSqlMapper mapper = new UserSqlMapper();

        public UserRepository(BloghostContext context) => this.context = context;

        public async void Add(UserInfo user)
        {
            await context.Users.AddAsync(new UserModel()
            {
                Email = user.Email,
                Nickname = user.Nickname,
                Password = user.Password
            });
            context.SaveChanges();
        }

        public UserInfo GetByEmail(string email) => mapper.GetDomain(context.Users.FirstOrDefault(model => model.Email == email));

        public UserInfo GetById(int id) => mapper.GetDomain(context.Users.FirstOrDefault(model => model.Id == id));

        public UserInfo GetByName(string name) => mapper.GetDomain(context.Users.FirstOrDefault(model => model.Nickname == name));
    }
}
