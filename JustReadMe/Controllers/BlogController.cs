using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using JustReadMe.ViewModels;
using JustReadMe.Models;
using JustReadMe.Interfaces.Repository;
using JustReadMe.DomainModels;

namespace JustReadMe.Controllers
{
    public class BlogController : Controller
    {
        private IUserRepository users;
        private IBlogsRepository blogs;
        private IPostRepository articles;

        public BlogController(IUserRepository users, IBlogsRepository blogs, IPostRepository articles)
        {
            this.users = users;
            this.blogs = blogs;
            this.articles = articles;
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
            ViewBag.Posts = articles.GetPostsByBlogId(blog.Id);
            return View(blog);
        }
    }
}
