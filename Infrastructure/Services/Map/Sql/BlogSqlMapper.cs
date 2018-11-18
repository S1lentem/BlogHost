using BlogHostCore.DomainModels;
using Infrastructure.Models;

namespace Infrastructure.Services.Map.Sql
{
    public class BlogSqlMapper
    {
        public Blog GetDomain(BlogModel model) => new Blog(model.Id, model.Title, model.Description, model.DateCreate);
    }
}
