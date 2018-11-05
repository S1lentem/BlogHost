using JustReadMe.DomainModels;
using JustReadMe.Interfaces.Repository;
using JustReadMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JustReadMe.Services.Map.Sql
{
    public class BlogSqlMapper
    {
        public Blog GetDomain(BlogModel model) => new Blog(model.Id, model.Title, model.Description, model.DateCreate);
    }
}
