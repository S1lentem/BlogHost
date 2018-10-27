using Microsoft.Extensions.DependencyInjection;
using JustReadMe.Interfaces;
using JustReadMe.Protection;

namespace JustReadMe.Extension.RegisterServices
{
    public static class HashService
    {
        public static void AddPasswordHash(this IServiceCollection services) => services.AddScoped<IHashable, PasswordHashingManager>();
    }
}
