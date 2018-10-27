using Microsoft.Extensions.DependencyInjection;
using JustReadMe.Interfaces.Services;
using JustReadMe.Services;

namespace JustReadMe.Extension.RegisterServices
{
    public static class AuthRegisterServices
    {
        public static void AddAuthRegisterService(this IServiceCollection services)
            => services.AddScoped<IAuthenticationRegisterService, AuthenticationRegisterService>();
    }
}
