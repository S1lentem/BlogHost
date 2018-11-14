using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JustReadMe.Interfaces.Repository;
using JustReadMe.ViewModels;

namespace JustReadMe.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IPostRepository posts;
        private readonly IBlogsRepository blogs;
        private readonly ICommentRepository comments;
        private readonly IUserRepository users;

        public ArticleController(IPostRepository posts, IBlogsRepository blogs, ICommentRepository comments, IUserRepository users)
        {
            this.posts = posts;
            this.blogs = blogs;
            this.comments = comments;
            this.users = users;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index() => View(posts.GetAllByUserName(User.Identity.Name));

        [HttpGet]
        public IActionResult SearchByTag() => View();

        [HttpPost]
        public IActionResult SearchByTag(string tag) => View(posts.GetAllByTag(tag));

        [HttpGet]
        public IActionResult PostArticle() => PartialView();


        [HttpGet]
        [Authorize]
        public IActionResult Create(string blog)
        {
            ViewBag.blogTitle = blog;
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(string blog, ArticleCreateModel model)
        {
            posts.CreatePostAsync(blog, User.Identity.Name, model);
            return RedirectToAction("ShowBlog", "Blog", new { title = blog });
        }

        [HttpGet]
        public IActionResult Show(int id) => View(posts.GetPostByIdWithComments(id));
        

        [HttpPost]
        public IActionResult Show(int id, string message)
        {
            comments.SendComment(message, User.Identity.Name, id);
            return RedirectToAction("Show", new { id });
        }
    }
}
