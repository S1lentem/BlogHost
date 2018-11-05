using JustReadMe.DomainModels;
using JustReadMe.Interfaces.Repository;
using JustReadMe.Models;
using JustReadMe.Services.Map.Sql;
using JustReadMe.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JustReadMe.Storages.Sql
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly BloghostContext context;
        private readonly PostSqlMapper mapper = new PostSqlMapper();

        public ArticleRepository(BloghostContext context) => this.context = context;

        public void CreatePostAsync(string blog, string userName, ArticleCreateModel model)
        {
            var blogInfo = context.Blogs.FirstOrDefault(blogModel => blogModel.UserModel.Nickname == userName && blogModel.Title == blog);
            context.BlogArticles.Add(new PostModel()
            {
                Tag = model.Tag,
                Message = model.Message,
                BlogModel = blogInfo,
                DateCreation = DateTime.Now
            });
            context.SaveChanges();
        }

        
        public IEnumerable<Post> GetAllByTag(string tag)
            => context.BlogArticles.Where(model => model.Tag == tag)
            .Include(model => model.BlogModel.UserModel)
            .Select(model => mapper.GetDomain(model));


        public IEnumerable<Post> GetAllByUserName(string userName)
            => context.BlogArticles.Where(model => model.BlogModel.UserModel.Nickname == userName)
            .Include(model => model.BlogModel.UserModel)
            .Select(model => mapper.GetDomain(model));


        public Post GetById(int id) 
            => mapper.GetDomain(context.BlogArticles.Include(model => model.BlogModel.UserModel).FirstOrDefault(model => model.Id == id));


        public IEnumerable<Post> GetPostsByBlogId(int id)
            => context.BlogArticles.Where(model => model.BlogModel.Id == id)
            .Include(model => model.BlogModel.UserModel)
            .Select(model => mapper.GetDomain(model));
    }
}
