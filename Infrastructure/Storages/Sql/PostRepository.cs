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
    public class PostRepository : IPostRepository
    {
        private readonly BloghostContext context;
        private readonly PostSqlMapper mapper = new PostSqlMapper();

        public PostRepository(BloghostContext context) => this.context = context;

        public void CreatePostAsync(string blog, string userName, string title, string message)
        {
            var blogInfo = context.Blogs.FirstOrDefault(blogModel => blogModel.UserModel.Nickname == userName && blogModel.Title == blog);
            var tagModel = context.Tags.FirstOrDefault(t => t.Title == title) ?? new TagModel() { Title = title };
            var postModel = new PostModel()
            {
                Title = title,
                Message = message,
                BlogModel = blogInfo,
                DateCreation = DateTime.Now,
            };

            context.Posts.Add(postModel);
            postModel.PostTag.Add(new PostTagModel() { Post = postModel, Tag = tagModel});
            context.SaveChanges();
        }

        
        public IEnumerable<Post> GetDescriptionAllPostByTag(string tag)
        {
            var tagInfo = context.Tags
                .Include(model => model.PostTag)
                .ThenInclude(model => model.Post.BlogModel.UserModel)
                .FirstOrDefault(model => model.Title == tag);

            return tagInfo?.PostTag.Select(postTag => mapper.GetDomain(postTag.Post, null));
        }
        

        public IEnumerable<Post> GetDescriptionAllByUserName(string userName)
            => context.Posts.Where(model => model.BlogModel.UserModel.Nickname == userName)
            .Include(model => model.BlogModel.UserModel)
            .Select(model => mapper.GetDomain(model, null));


        public Post GetById(int id) 
            => mapper.GetDomain(context.Posts.Include(model => model.BlogModel.UserModel).FirstOrDefault(model => model.Id == id));

        public Post GetFullPostById(int id)
        {
           
            var commentModels = context.Сomments.Where(model => model.Post.Id == id)
                .Include(model => model.Post.BlogModel.UserModel)
                .ToList();


            return commentModels.Count != 0 ? mapper.GetDomain(commentModels[0].Post, commentModels) : GetById(id);
        }

        public IEnumerable<Post> GetDescriptionPostsByBlogId(int id)
            => context.Posts.Where(model => model.BlogModel.Id == id)
            .Include(model => model.BlogModel.UserModel)
            .Select(model => mapper.GetDomain(model, null));
    }
}
