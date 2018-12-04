using BlogHostCore.DomainModels;
using BlogHostCore.Interfaces.Repository;
using Infrastructure.Models;
using Infrastructure.Services.Map.Sql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Storages.Sql
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
                .Include(model => model.Post)
                .FirstOrDefault(model => model.Id == id));

        public IEnumerable<Comment> GetByPostId(int postId)
            => context.Сomments.Where(model => model.Post.Id == postId)
            .Include(model => model.User)
            .Include(model => model.Post)
            .Select(model => mapper.GetDomain(model))
            .ToList();

       
        public void SendComment(string message, string userName, int postId)
        {
            context.Сomments.Add(new CommentModel()
            {
                Post = context.Posts.FirstOrDefault(model => model.Id == postId),
                User = context.Users.FirstOrDefault(model => model.Nickname == userName),
                Message = message,
                DateCreate = DateTime.Now
            });
            context.SaveChanges();
        }

        public void RemoveById(int id)
        {
            var comment = context.Сomments.FirstOrDefault(model => model.Id == id);
            if (comment != null)
            {
                context.Сomments.Remove(comment);
                context.SaveChanges();
            }
        }
    }
}
