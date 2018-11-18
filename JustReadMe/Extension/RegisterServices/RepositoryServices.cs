using Microsoft.Extensions.DependencyInjection;
using BlogHostCore.Interfaces.Repository;
using Infrastructure.Storages.Sql;
using Infrastructure.Storages;

namespace Web.Extension.RegisterServices
{
    public static class RepositoryServices
    {
        public static void AddSqlUserRepository(this IServiceCollection services) => services.AddScoped<IUserRepository, UserRepository>();

        public static void AddSqlBlogRepository(this IServiceCollection services) => services.AddScoped<IBlogsRepository, BlogsRepository>();

        public static void AddSqlArticleRepository(this IServiceCollection services) 
            => services.AddScoped<IPostRepository, PostRepository>();

        public static void AddSqlCommentsRepository(this IServiceCollection services)
            => services.AddScoped<ICommentRepository, CommentRepository>();
    }
}
