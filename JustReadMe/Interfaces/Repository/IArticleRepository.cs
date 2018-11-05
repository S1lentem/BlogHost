﻿using JustReadMe.DomainModels;
using JustReadMe.Models;
using JustReadMe.ViewModels;
using System.Collections.Generic;

namespace JustReadMe.Interfaces.Repository
{
    public interface IArticleRepository : IBaseRepository<Post>
    {
        void CreatePostAsync(string blog, string userName, ArticleCreateModel model);

        IEnumerable<Post> GetPostsByBlogId(int id);

        IEnumerable<Post> GetAllByUserName(string userName);

        IEnumerable<Post> GetAllByTag(string tag);
    }
}
