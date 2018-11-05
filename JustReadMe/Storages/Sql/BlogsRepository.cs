using JustReadMe.DomainModels;
using JustReadMe.Interfaces;
using JustReadMe.Interfaces.Repository;
using JustReadMe.Models;
using JustReadMe.Services.Map.Sql;
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
        private readonly BloghostContext context;
        private readonly BlogSqlMapper mapper = new BlogSqlMapper();
        private readonly IUserRepository users;

        public BlogsRepository(BloghostContext context, IUserRepository users)
        {
            this.context = context;
            this.users = users;
        }

        public void AddBlog(string title, string description, string userName)
        {
            context.Blogs.Add(new BlogModel()
            {
                Title = title,
                Description = description,
                DateCreate = DateTime.Now,
                UserModel = users.GetByName(userName)
            });
            context.SaveChanges();
        }

        public Blog GetBlogByUserNameAndTitle(string userName, string title) =>
            mapper.GetDomain(context.Blogs.FirstOrDefault(model => model.Title == title && model.UserModel.Nickname == userName));

        public IEnumerable<Blog> GetBlogsByUserName(string userName)
            => context.Blogs.Where(model => model.UserModel.Nickname == userName).Select(model => mapper.GetDomain(model));

        public Blog GetById(int id) => mapper.GetDomain(context.Blogs.FirstOrDefault(model => model.Id == id));

   

    }
}
