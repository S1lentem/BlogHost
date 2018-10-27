using JustReadMe.Interfaces.Repository;
using JustReadMe.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JustReadMe.Storages.Sql
{
    public class BlogsRepository : IBlogsRepository
    {
        private BloghostContext context;

        public BlogsRepository(BloghostContext context) => this.context = context;

        public async void Add(BlogModel item)
        {
            await context.Blogs.AddAsync(item);
            context.SaveChanges();
        }

        public async Task<BlogModel> Find(Expression<Func<BlogModel, bool>> predicate) => await context.Blogs.FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<BlogModel>> GetAll(Expression<Func<BlogModel, bool>> predicate) =>
            await Task.Run(() => context.Blogs.Where(predicate));

        public void Remove(BlogModel item)
        {
            context.Blogs.Remove(item);
            context.SaveChanges();
        }

        public void Update(BlogModel item)
        {
            context.Blogs.Update(item);
            context.SaveChanges();
        }
    }
}
