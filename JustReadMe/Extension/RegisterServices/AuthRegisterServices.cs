using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Services;
using BlogHostCore.Interfaces.Services;

namespace Web.Extension.RegisterServices
{
    public static class AuthRegisterServices
    {
        public static void AddAuthRegisterService(this IServiceCollection services)
            => services.AddScoped<IAuthenticationRegisterService, AuthenticationRegisterService>();
    }
}
