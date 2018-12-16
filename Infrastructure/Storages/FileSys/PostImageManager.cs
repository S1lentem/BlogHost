using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using BlogHostCore.Interfaces;

namespace Infrastructure.Storages.FileSys
{
    public class PostImageManager : IImageStorable
    {
        public string Root { get; set; }
               
        public async Task<string> SaveImage(IFormFile file)
        {
            Guid guid = Guid.NewGuid();
            string uniqName = Convert.ToBase64String(guid.ToByteArray())
                .Replace("\\", "").Replace("/","").Replace("=", "").Replace("+", "");
            string fileName = Path.ChangeExtension(uniqName, "jpg");
            string path = Path.Combine(Root, fileName);
            try
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                File.Delete(path);
                throw ex;
            }
            return fileName;
        }

        public async Task<IEnumerable<string>> SaveImagesAsync(IEnumerable<IFormFile> files)
        {
            List<string> allFileNames = new List<string>();
            if (files != null)
            {
                foreach (var file in files)
                {
                    allFileNames.Add(await SaveImage(file));
                }
            }
            return allFileNames;
        }

        public void RemoveFileForPost(params string[] imageNames)
        {
            foreach (var imageName in imageNames)
            {
                string path = Path.Combine(Root, imageName);
                try
                {
                    File.Delete(path);
                }
                catch (FileNotFoundException) { }
                catch (DirectoryNotFoundException) { }
            }
        }
    }
}
