using BlogHostCore.Interfaces;
using Infrastructure.Storages.FileSys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Extension.RegisterServices
{
    public static class FilesServices
    {
        public static void AddFileImageManager(this IServiceCollection services) => services.AddScoped<IImageStorable, PostImageManager>();

    }
}
