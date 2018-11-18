using BlogHostCore.DomainModels;
using System.Collections.Generic;

namespace BlogHostCore.Interfaces.Repository
{
    public interface IBlogsRepository : IBaseRepository<Blog>
    {
        IEnumerable<Blog> GetBlogsByUserName(string userName);

        Blog GetBlogByUserNameAndTitle(string userName, string title);

        void AddBlog(string title, string description, string userName);
    }
}
