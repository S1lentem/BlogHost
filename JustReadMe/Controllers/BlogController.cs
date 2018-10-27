using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using JustReadMe.ViewModels;
using JustReadMe.Models;
using JustReadMe.Interfaces.Repository;

namespace JustReadMe.Controllers
{
    public class BlogController : Controller
    {
        private IUserRepository users;
        private IBlogsRepository blogs;
        private IArticleRepository articles;

        public BlogController(IUserRepository users, IBlogsRepository blogs, IArticleRepository articles)
        {
            this.users = users;
            this.blogs = blogs;
            this.articles = articles;
        }

        [Authorize]
        public IActionResult Index() =>
            View(blogs.GetAll(model => model.UserModel.Email == User.Identity.Name).Result);


        [HttpGet]
        [Authorize]
        public IActionResult CreateBlog() => View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBlog(CreateBlogModel model)
        {
            var currentUser = await users.Find(userModel => userModel.Email == User.Identity.Name);
            blogs.Add(new BlogModel()
            {
                Title = model.Title,
                Description = model.Description,
                DateCreate = DateTime.Now,
                UserModel = currentUser
            });
            return RedirectToAction("Index");
        }
    }
}
