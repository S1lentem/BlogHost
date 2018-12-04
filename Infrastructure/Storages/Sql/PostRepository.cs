using BlogHostCore.DomainModels;
using BlogHostCore.Interfaces.Repository;
using Infrastructure.Models;
using Infrastructure.Models.PostComponents;
using Infrastructure.Services.Map.Sql;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Infrastructure.Storages.Sql
{
    public class PostRepository : IPostRepository
    {
        private readonly BloghostContext context;
        private readonly PostSqlMapper mapper = new PostSqlMapper();

        public PostRepository(BloghostContext context) => this.context = context;

        /*
        public void CreatePostAsync(string blog, string userName, string title, string message, string[] tags)
        {
            var blogInfo = context.Blogs.FirstOrDefault(blogModel => blogModel.UserModel.Nickname == userName && blogModel.Title == blog);
            var postModel = new PostModel()
            {
                Title = title,
                Message = message,
                BlogModel = blogInfo,
                DateCreation = DateTime.Now,
            };
            context.Posts.Add(postModel);

            foreach (var tag in tags)
            {
                var tagModel = context.Tags.FirstOrDefault(t => t.Title == tag) ?? new TagModel() { Title = tag };
                context.Add(new PostTagModel() { Post = postModel, Tag = tagModel });
            }
            context.SaveChanges();
        }*/


        public IEnumerable<Post> GetDescriptionAllPostByTag(string tag)
        {
            var tagInfo = context.Tags
                .Include(model => model.PostTag)
                .ThenInclude(model => model.Post.BlogModel.UserModel)
                .FirstOrDefault(model => model.Title == tag);

            return tagInfo?.PostTag.Select(postTag => mapper.GetDomain(postTag.Post, null, null));
        }
        

        public IEnumerable<Post> GetDescriptionAllByUserName(string userName)
            => context.Posts.Where(model => model.BlogModel.UserModel.Nickname == userName)
            .Include(model => model.BlogModel.UserModel)
            .Select(model => mapper.GetDomain(model, null, null, null));


        public Post GetById(int id)
        {
            var postModel = context
                  .Posts.Include(model => model.BlogModel.UserModel)
                  .Include(model => model.PostTag)
                  .ThenInclude(model => model.Tag)
                  .FirstOrDefault(model => model.Id == id);

            var textComponents = context.TextComponents.Where(model => model.Post == postModel);
            var imageComponents = context.ImageComponents.Where(model => model.Post == postModel);

            return mapper.GetDomain(postModel, textComponents, imageComponents);
        }
        public Post GetFullPostById(int id)
        {
            var commentModels = context.Сomments.Where(model => model.Post.Id == id)
                .Include(model => model.Post.BlogModel.UserModel)
                .ToList();

            var textComponents = context.TextComponents.Where(model => model.Post == commentModels[0].Post);
            var imageComponents = context.ImageComponents.Where(model => model.Post == commentModels[0].Post);

            return commentModels.Count != 0 ? 
                mapper.GetDomain(commentModels[0].Post, textComponents, imageComponents, commentModels) : GetById(id);


        }

        public IEnumerable<Post> GetDescriptionPostsByBlogId(int id)
            => context.Posts.Where(model => model.BlogModel.Id == id)
            .Include(model => model.BlogModel.UserModel)
            .Select(model => mapper.GetDomain(model, null, null, null));

        public  void RemoveById(int id)
        {
            var post = context.Posts.FirstOrDefault(model => model.Id == id);
            if (post != null)
            {
                context.Posts.Remove(post);
                context.SaveChanges();
            }
        }

        public void Add(string title, string userName, string blog, string[] tags, string[] texts, List<IFormFile> files, string[] order)
        {
            var blogInfo = context.Blogs.FirstOrDefault(blogModel => blogModel.UserModel.Nickname == userName && blogModel.Title == blog);
            var postModel = new PostModel()
            {
                Title = title,
                BlogModel = blogInfo,
                DateCreation = DateTime.Now,
            };
            context.Posts.Add(postModel);

            foreach (var tag in tags)
            {
                var tagModel = context.Tags.FirstOrDefault(t => t.Title == tag) ?? new TagModel() { Title = tag };
                context.Add(new PostTagModel() { Post = postModel, Tag = tagModel });
            }
        
            int textIndex = 0, imageIndex = 0;
            for (int i = 0; i < order.Length; i++)
            {
                if (order[i] == "text")
                {
                    context.TextComponents.Add(new TextComponent()
                    {
                        OrderNum = i,
                        Post = postModel,
                        Text = texts[textIndex++]
                    });
                }
                if (order[i] == "image")
                {
                    context.ImageComponents.Add(new ImageComponent()
                    {
                        OrderNum = i,
                        Post = postModel,
                        ImagePath = Path.Combine("wwwroot", "Files", userName, blog, title, files[imageIndex++].FileName)
                    });
                }
            }

            context.SaveChanges();
        }
    }
}
