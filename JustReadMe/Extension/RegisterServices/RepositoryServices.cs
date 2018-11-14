using Microsoft.Extensions.DependencyInjection;
using JustReadMe.Storages.Sql;
using JustReadMe.Storages;
using JustReadMe.Interfaces.Repository;

namespace JustReadMe.Extension.RegisterServices
{
    public static class RepositoryServices
    {
        public static void AddSqlUserRepository(this IServiceCollection services) => services.AddScoped<IUserRepository, UserRepository>();

        public static void AddSqlBlogRepository(this IServiceCollection services) => services.AddScoped<IBlogsRepository, BlogsRepository>();

        public static void AddSqlArticleRepository(this IServiceCollection services) 
            => services.AddScoped<IPostRepository, ArticleRepository>();

        public static void AddSqlCommentsRepository(this IServiceCollection services)
            => services.AddScoped<ICommentRepository, CommentRepository>();
    }
}
