using Microsoft.Extensions.DependencyInjection;
using BlogHostCore.Interfaces;

using SupportLibrary.Protection;

namespace Web.Extension.RegisterServices
{
    public static class HashService
    {
        public static void AddPasswordHash(this IServiceCollection services) => services.AddScoped<IHashable, PasswordHashingManager>();
    }
}
