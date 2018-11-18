using BlogHostCore.DomainModels;
using System.Collections.Generic;

namespace BlogHostCore.Interfaces.Repository
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        void SendComment(string comment, string userName, int postId);

        IEnumerable<Comment> GetByPostId(int postId);
    }
}
