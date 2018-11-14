using JustReadMe.DomainModels;
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
    public class CommentRepository : ICommentRepository
    {
        private readonly BloghostContext context;
        private readonly IUserRepository users;
        private readonly IPostRepository posts;
        private readonly CommentSqlMapper mapper = new CommentSqlMapper();

        public CommentRepository(BloghostContext context, IUserRepository users, IPostRepository posts)
        {
            this.context = context;
            this.posts = posts;
            this.users = users;
        }

        public Comment GetById(int id) 
            => mapper.GetDomain(context.Сomments
                .Include(model => model.User)
                .Include(model => model.Article)
                .FirstOrDefault(model => model.Id == id));

        public IEnumerable<Comment> GetByPostId(int postId)
            => context.Сomments.Where(model => model.Article.Id == postId)
            .Include(model => model.User)
            .Include(model => model.Article)
            .Select(model => mapper.GetDomain(model));

       
        public void SendComment(string message, string userName, int postId)
        {
            context.Сomments.Add(new CommentModel()
            {
                Article = context.BlogArticles.FirstOrDefault(model => model.Id == postId),
                User = context.Users.FirstOrDefault(model => model.Nickname == userName),
                Message = message,
                DateCreate = DateTime.Now
            });
            context.SaveChanges();
        }
    }
}
