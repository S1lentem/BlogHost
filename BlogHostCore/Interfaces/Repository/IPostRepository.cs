using BlogHostCore.DomainModels;
using System.Collections.Generic;

namespace BlogHostCore.Interfaces.Repository
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        void CreatePostAsync(string blog, string userName, string title, string message);

        IEnumerable<Post> GetDescriptionPostsByBlogId(int id);

        Post GetFullPostById(int id);

        IEnumerable<Post> GetDescriptionAllByUserName(string userName);

        IEnumerable<Post> GetDescriptionAllPostByTag(string tag);
    }
}
