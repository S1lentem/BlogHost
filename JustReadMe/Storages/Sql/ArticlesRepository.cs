using JustReadMe.Interfaces.Repository;
using JustReadMe.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JustReadMe.Storages.Sql
{
    public class ArticleRepository : IArticleRepository
    {
        private BloghostContext context;

        public ArticleRepository(BloghostContext context) => this.context = context;

        public async void Add(BlogArticleModel item)
        {
            await context.BlogArticles.AddAsync(item);
            context.SaveChanges();
        }

        public async Task<BlogArticleModel> Find(Expression<Func<BlogArticleModel, bool>> predicate) 
            => await context.BlogArticles.FirstOrDefaultAsync(predicate);


        public async Task<IEnumerable<BlogArticleModel>> GetAll(Expression<Func<BlogArticleModel, bool>> predicate) 
            => await Task.Run(() => context.BlogArticles.Where(predicate));


        public void Remove(BlogArticleModel item)
        {
            context.BlogArticles.Remove(item);
            context.SaveChanges();
        }
        
        public void Update(BlogArticleModel item) {
            context.BlogArticles.Update(item);
            context.SaveChanges();
        }


    }
}
