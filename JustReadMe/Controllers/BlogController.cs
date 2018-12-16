using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlogHostCore.Interfaces.Repository;
using Web.ViewModels;
using Infrastructure.Storages.FileSys;
using BlogHostCore.DomainModels;
using BlogHostCore.Interfaces;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogsRepository blogs;
        private readonly IPostRepository articles;
        private readonly IImageStorable imageStorable;

        public BlogController(IUserRepository users, IBlogsRepository blogs, IPostRepository articles, IImageStorable imageStorable,
            IConfiguration configuration)
        {
            this.blogs = blogs;
            this.articles = articles;
            this.imageStorable = imageStorable;
            this.imageStorable.Root = configuration["ImageRoot"];
        }

        [Authorize]
        public IActionResult Index() => View(blogs.GetBlogsByUserName(User.Identity.Name));


        [HttpGet]
        [Authorize]
        public IActionResult CreateBlog() => View();

        [HttpPost]
        [Authorize]
        public IActionResult CreateBlog(CreateBlogModel model)
        {
            blogs.AddBlog(model.Title, model.Description, User.Identity.Name);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ShowBlog(string title)
        {
            var blog = blogs.GetBlogByUserNameAndTitle(User.Identity.Name, title);
            ViewBag.Posts = articles.GetDescriptionPostsByBlogId(blog.Id);
            return View(blog);
        }

       
        [Authorize]
        public IActionResult RemoveBlogById(int id)
        {
            var allImages = blogs.GetAllImagesByBlogId(id);
            imageStorable.RemoveFileForPost(allImages.ToArray());
            blogs.RemoveById(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            return View(blogs.GetById(id));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(string title, string desc, int id)
        {
            blogs.UpdateBlog(title, desc, id);
            return RedirectToAction("Index");
        }
    }
}
