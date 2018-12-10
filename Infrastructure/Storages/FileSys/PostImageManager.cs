using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace Infrastructure.Storages.FileSys
{
    public class PostImageManager
    {
        private readonly string root = "wwwroot";
        private readonly string container = "files";

        public async Task<bool> SaveImage(IFormFile file, string userName, string blogName, string postName)
        {
            if (file == null)
            {
                return false;
            }
            string pathForPost = Path.Combine(root, container, userName, blogName, postName);
            try
            {
                if (!Directory.Exists(pathForPost))
                {
                    Directory.CreateDirectory(pathForPost);
                }

                using (var fileStream = new FileStream(Path.Combine(pathForPost, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                Directory.Delete(pathForPost, true);
                throw ex;
            }
            return true;
        }

        public async Task<bool> SaveImages(List<IFormFile> files, string userName, string blogName, string postName)
        {
            bool result = false;
            foreach(var file in files)
            {
                result = await SaveImage(file, userName, blogName, postName);
                if (!result) break;
            }
            return result;
        }

        public void RemoveFileForPost(string userName, string blogTitle, string postTitle)
        {
            string path = Path.Combine(root, container, userName, blogTitle, postTitle);
            try
            {
                Directory.Delete(path, true);
            }
            catch (DirectoryNotFoundException) { }
        }
    }
}
