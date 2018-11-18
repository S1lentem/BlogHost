using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlogHostCore.Interfaces.Repository;
using Web.ViewModels;

namespace Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository posts;
        private readonly IBlogsRepository blogs;
        private readonly ICommentRepository comments;
        private readonly IUserRepository users;

        public PostController(IPostRepository posts, IBlogsRepository blogs, ICommentRepository comments, IUserRepository users)
        {
            this.posts = posts;
            this.blogs = blogs;
            this.comments = comments;
            this.users = users;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index() => View(posts.GetDescriptionAllByUserName(User.Identity.Name));

        [HttpGet]
        public IActionResult SearchByTag() => View();

        [HttpPost]
        public IActionResult SearchByTag(string tag) => View(posts.GetDescriptionAllPostByTag(tag));

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
        public IActionResult Create(string blog, PostCreateModel model)
        {
            posts.CreatePostAsync(blog, User.Identity.Name, model.Title, model.Message);
            return RedirectToAction("ShowBlog", "Blog", new { title = blog });
        }

        [HttpGet]
        public IActionResult Show(int id) => View(posts.GetFullPostById(id));
        

        [HttpPost]
        public IActionResult Show(int id, string message)
        {
            comments.SendComment(message, User.Identity.Name, id);
            return RedirectToAction("Show", new { id });
        }
    }
}
