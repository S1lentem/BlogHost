using BlogHostCore.DomainModels;
using Infrastructure.Models;

namespace Infrastructure.Services.Map.Sql
{
    public class CommentSqlMapper
    {
        public Comment GetDomain(CommentModel model) => new Comment(model.Id, model.User.Nickname, model.Post.Id, model.DateCreate, model.Message);
    }
}
