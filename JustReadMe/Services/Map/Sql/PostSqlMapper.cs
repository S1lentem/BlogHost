using JustReadMe.DomainModels;
using JustReadMe.Models;

namespace JustReadMe.Services.Map.Sql
{
    public class PostSqlMapper
    {
        public Post GetDomain(PostModel model) 
            => new Post(model.Id, model.Tag, model.Message, model.DateCreation, model.DateChange, model.BlogModel.UserModel.Nickname);
    }
}
