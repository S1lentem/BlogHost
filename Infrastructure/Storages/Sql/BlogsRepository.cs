using BlogHostCore.DomainModels;
using BlogHostCore.Interfaces.Repository;
using Infrastructure.Models;
using Infrastructure.Services.Map.Sql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Storages.Sql
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
                UserModel = context.Users.FirstOrDefault(model => model.Nickname == userName)
            });
            context.SaveChanges();
        }

        public IEnumerable<string> GetAllImagesByBlogId(int id)
            => context.ImageComponents.Where(model => model.Post.BlogModel.Id == id).Select(model => model.ImagePath);

        public Blog GetBlogByUserNameAndTitle(string userName, string title) =>
            mapper.GetDomain(context.Blogs.FirstOrDefault(model => model.Title == title && model.UserModel.Nickname == userName));

        public IEnumerable<Blog> GetBlogsByUserName(string userName)
            => context.Blogs.Where(model => model.UserModel.Nickname == userName).Select(model => mapper.GetDomain(model));

        public Blog GetById(int id) => mapper.GetDomain(context.Blogs.FirstOrDefault(model => model.Id == id));

        public void RemoveById(int id)
        {
            var model = context.Blogs.FirstOrDefault(item => item.Id == id);
            context.Blogs.Remove(model);
            context.SaveChanges();
        }

        public void UpdateBlog(string title, string desc, int id)
        {
            var blog = context.Blogs.FirstOrDefault(model => model.Id == id);
            blog.Title = title;
            blog.Description = desc;
            context.Blogs.Update(blog);
            context.SaveChanges();
        }

    
    }
}
