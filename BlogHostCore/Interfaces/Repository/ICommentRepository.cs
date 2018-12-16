using BlogHostCore.DomainModels;
using System.Collections.Generic;

namespace BlogHostCore.Interfaces.Repository
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Comment SendComment(string comment, string userName, int postId);

        IEnumerable<Comment> GetByPostId(int postId);

        void RemoveById(int id);
    }
}
