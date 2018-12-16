using BlogHostCore.DomainModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BlogHostCore.Interfaces.Repository
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        void Add(string title, string userName, string blog, string[] tags, string[] texts, string[] images, string[] order);
        IEnumerable<Post> GetDescriptionPostsByBlogId(int id);
        Post GetFullPostById(int id);
        IEnumerable<Post> GetDescriptionAllByUserName(string userName);
        IEnumerable<Post> GetDescriptionAllPostByTag(string tag);
        IEnumerable<Post> GetDescriptionAllPostContainsInTitle(string partTitle, string userName);
        void RemoveById(int id);
        string GetBlogTitleForPostId(int id);
        IEnumerable<string> GetAllImagesNamesByPostId(int id);
    }
}
