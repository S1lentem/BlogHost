using JustReadMe.DomainModels;
using JustReadMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustReadMe.Services.Map.Sql
{
    public class CommentSqlMapper
    {
        public Comment GetDomain(CommentModel model) => new Comment(model.Id, model.User.Nickname, model.Article.Id, model.DateCreate, model.Message);
    }
}
