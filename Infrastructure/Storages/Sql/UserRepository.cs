using System.Linq;
using Infrastructure.Models;
using BlogHostCore.DomainModels;
using BlogHostCore.Interfaces.Repository;
using Infrastructure.Services.Map.Sql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Infrastructure.Storages.Sql
{
    public class UserRepository : IUserRepository
    {
        private BloghostContext context;
        private UserSqlMapper mapper = new UserSqlMapper();

        public UserRepository(BloghostContext context) => this.context = context;

        public void Add(UserInfo user, string role = "User")
        {
            var userRole = context.Roles.FirstOrDefault(model => model.Name == role);
            context.Users.AddAsync(new UserModel()
            {
                Email = user.Email,
                Nickname = user.Nickname,
                Password = user.Password,
                Role = userRole
            });
            context.SaveChanges();
        }

        public IEnumerable<UserInfo> GetFullInfoForAllUsers()
        {
            var users = context.Users.Include(model => model.Role);
            var allUsersInfo = new List<UserInfo>();
            foreach(var user in users)
            {
                var userInfo = mapper.GetDomain(user);
                userInfo.Role = (UserRole)Enum.Parse(typeof(UserRole), user.Role.Name);
                allUsersInfo.Add(userInfo);
            }
            return allUsersInfo;

        }
           


        public UserInfo GetFullInfoByEmail(string email)
        {
            var user = context.Users
                .Include(model => model.Role)
                .FirstOrDefault(model => model.Email == email);
            if (user == null) return null;
            var userInfo = mapper.GetDomain(user);
            userInfo.Role = (UserRole)Enum.Parse(typeof(UserRole), user.Role.Name);
            return userInfo;
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

        public void SetRole(string userName, string role)
        {
            var userModel = context.Users.FirstOrDefault(model => model.Nickname == userName)
                ?? throw new ArgumentException($"User {userName} not found", userName);
            var roleModel = context.Roles.FirstOrDefault(model => model.Name == role)
                ?? throw new ArgumentException($"Role {role} not found", role);

            userModel.Role = roleModel;
            context.Users.Update(userModel);
            context.SaveChanges();
        }

        public UserInfo GetByEmail(string email)
        {
            var user = context.Users.FirstOrDefault(model => model.Email == email);
            return user != null ? mapper.GetDomain(user) : null;
        }
    }
}
