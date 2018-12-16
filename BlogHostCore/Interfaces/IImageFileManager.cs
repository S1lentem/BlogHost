using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHostCore.Interfaces
{
    public interface IImageStorable
    {
        Task<string> SaveImage(IFormFile file);
        Task<IEnumerable<string>> SaveImagesAsync(IEnumerable<IFormFile> files);
        void RemoveFileForPost(params string[] imageNames);
        string Root { get; set; }
    }
}

