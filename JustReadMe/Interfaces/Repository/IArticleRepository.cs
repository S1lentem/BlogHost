using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JustReadMe.Models;

namespace JustReadMe.Interfaces.Repository
{
    public interface IArticleRepository : IBaseRepository<BlogArticleModel>
    {
        void CreatePost((string message, string tag) info, BlogModel model);
    }
}
