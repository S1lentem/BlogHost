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


        public async Task<UserModel> Find(Expression<Func<UserModel, bool>> predicate) => await context.Users.FirstOrDefaultAsync(predicate);


        public async Task<IEnumerable<UserModel>> GetAll(Expression<Func<UserModel, bool>> predicate) => await Task.Run(() => context.Users.Where(predicate));


        public void Remove(UserModel item)
        {
            context.Users.Remove(item);
            context.SaveChanges();
        }


        public void Update(UserModel item)
        {
            context.Users.Update(item);
            context.SaveChanges();
        }
    }
}
