using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

using Infrastructure.Models;
using Web.Extension.RegisterServices;
using BlogHostCore.Interfaces.Repository;
using BlogHostCore.Interfaces.Services;
using BlogHostCore.Interfaces;
using Web.Hubs;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace JustReadMe
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BloghostContext>(options => options.UseSqlServer(connection));
           

            services.AddSqlUserRepository();
            services.AddSqlBlogRepository();
            services.AddSqlArticleRepository();
            services.AddSqlCommentsRepository();

            services.AddPasswordHash();
            services.AddAuthRegisterService();

            services.AddFileImageManager();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.LoginPath = new PathString("/Home/Index"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddViewLocalization();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IUserRepository users,
            IAuthenticationRegisterService registration, IHashable hashManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseAuthentication();
            app.UseFileServer();

           
            app.UseSignalR(route => route.MapHub<NotificationHub>("/notification"));

            var supportedCulture = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = supportedCulture,
                SupportedUICultures = supportedCulture
            });
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            if (users.GetByName("Admin") == null) registration.CreateNewUser("Admin", "Admin@gmail.com", "Admin", hashManager, "Admin");
        }
    }
}
