using BlogHostCore.DomainModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BlogHostCore.Interfaces.Repository
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        //void CreatePostAsync(string blog, string userName, string title, string message, string[] tags);
        void Add(string title, string userName, string blog, string[] tags, string[] texts, List<IFormFile> files, string[] order);

        IEnumerable<Post> GetDescriptionPostsByBlogId(int id);

        Post GetFullPostById(int id);

        IEnumerable<Post> GetDescriptionAllByUserName(string userName);

        IEnumerable<Post> GetDescriptionAllPostByTag(string tag);

        void RemoveById(int id);
    }
}
