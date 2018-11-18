using System.Linq;
using Infrastructure.Models;
using BlogHostCore.DomainModels;
using BlogHostCore.Interfaces.Repository;
using Infrastructure.Services.Map.Sql;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Storages.Sql
{
    public class UserRepository : IUserRepository
    {
        private BloghostContext context;
        private UserSqlMapper mapper = new UserSqlMapper();

        public UserRepository(BloghostContext context) => this.context = context;

        public void Add(UserInfo user)
        {
            var userRole = context.Roles.FirstOrDefault(model => model.Name == "User");
            context.Users.AddAsync(new UserModel()
            {
                Email = user.Email,
                Nickname = user.Nickname,
                Password = user.Password,
                Role = userRole
            });
            context.SaveChanges();
        }

        public UserInfo GetByEmail(string email)
        {
            var user = context.Users.FirstOrDefault(model => model.Email == email);
            return user != null ? mapper.GetDomain(user) : null;
        }

        public UserInfo GetById(int id)
        {
            var user = context.Users.FirstOrDefault(model => model.Id == id);
            return user != null ? mapper.GetDomain(user) : null;
        }

        public UserInfo GetByName(string name)
        {
            var user = context.Users.FirstOrDefault(model => model.Nickname == name);
            return user != null ? mapper.GetDomain(user) : null;
        }

        public UserInfo GetFullInfoByName(string name)
        {
            var userInfo = context.Users
                .Include(model => model.Role)
                .FirstOrDefault(model => model.Nickname == name);

            var user = mapper.GetDomain(userInfo);
            user.Role = (UserRole) Enum.Parse(typeof(UserRole), userInfo.Role.Name);
            return user;
        }

        public void SetRole(string userName, UserRole role)
        {
            var userModel = context.Users.FirstOrDefault(model => model.Nickname == userName);
            var roleModel = context.Roles.FirstOrDefault(model => model.Name == role.ToString());
            userModel.Role = roleModel;
            context.SaveChanges();
        }
    }
}
