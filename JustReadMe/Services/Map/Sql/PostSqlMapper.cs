using JustReadMe.DomainModels;
using JustReadMe.Models;
using System.Collections.Generic;
using System.Linq;

namespace JustReadMe.Services.Map.Sql
{
    public class PostSqlMapper
    {
        public Post GetDomain(PostModel model, IEnumerable<CommentModel> comments = default(IEnumerable<CommentModel>))
        {
            var commentMapper = new CommentSqlMapper();

            return new Post(model.Id, model.Tag, model.Message, model.DateCreation, model.DateChange, 
                model.BlogModel.UserModel.Nickname, comments?.Select(comment => commentMapper.GetDomain(comment)).ToList());
        }
    }
}
