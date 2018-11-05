using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

using JustReadMe.Interfaces.Repository;
using JustReadMe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace JustReadMe.Storages
{
    public class UserRepository : IUserRepository
    {
        private BloghostContext context;

        public UserRepository(BloghostContext context) => this.context = context;

        public async void Add(UserModel item)
        {
            await context.Users.AddAsync(item);
            context.SaveChanges();
        }

        public UserModel GetByEmail(string email) => context.Users.FirstOrDefault(model => model.Email == email);

        public UserModel GetById(int id) => context.Users.FirstOrDefault(model => model.Id == id);

        public UserModel GetByName(string name) => context.Users.FirstOrDefault(model => model.Nickname == name);
    }
}
