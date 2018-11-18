using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlogHostCore.Interfaces.Repository;
using Web.ViewModels;

namespace Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogsRepository blogs;
        private readonly IPostRepository articles;

        public BlogController(IUserRepository users, IBlogsRepository blogs, IPostRepository articles)
        {
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
            ViewBag.Posts = articles.GetDescriptionPostsByBlogId(blog.Id);
            return View(blog);
        }
    }
}
