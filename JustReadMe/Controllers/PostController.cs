using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlogHostCore.Interfaces.Repository;
using Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Collections.Generic;
using Infrastructure.Storages.FileSys;

using System.Linq;
using BlogHostCore.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository posts;
        private readonly IBlogsRepository blogs;
        private readonly ICommentRepository comments;
        private readonly IUserRepository users;
        private readonly IHostingEnvironment appEnvironment;
        private readonly IImageStorable imageStorable;

        public PostController(IPostRepository posts, IBlogsRepository blogs, ICommentRepository comments, IUserRepository users,
            IHostingEnvironment appEnvironment, IImageStorable imageStorable, IConfiguration configuration)
        {
            this.appEnvironment = appEnvironment;
            this.imageStorable = imageStorable;
            this.posts = posts;
            this.blogs = blogs;
            this.comments = comments;
            this.users = users;
            this.imageStorable.Root = configuration["ImageRoot"];
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index() => View(posts.GetDescriptionAllByUserName(User.Identity.Name));

      
        [HttpGet]
        public IActionResult SearchByTag(string tag) 
            => tag == null ? View() : View(posts.GetDescriptionAllPostByTag(tag));

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
        public async Task<IActionResult> Create(string blog, string[] texts, List<IFormFile> files, string[] order, PostCreateModel model)
        {
  
            var imageNames = await imageStorable.SaveImagesAsync(files);
            posts.Add(model.Title, User.Identity.Name, blog, model.Tags.Split(','), texts, imageNames.ToArray(), order);
            return RedirectToAction("ShowBlog", "Blog", new { title = blog });
        }

        [HttpGet]
        public IActionResult Show(int id) => View(posts.GetFullPostById(id));
        

        [HttpPost]
        [Authorize]
        public IActionResult SendComment(int id, string message)
        {
            comments.SendComment(message, User.Identity.Name, id);
            return RedirectToAction("Show", new { id });
        }

        [HttpGet]
        [Authorize]
        public IActionResult RemoveComment(int id, int postId)
        {
            comments.RemoveById(id);
            return RedirectToAction("Show", new { id = postId });
        }

        [HttpGet]
        [Authorize]
        public IActionResult RemovePost(int id)
        {
            var post = posts.GetFullPostById(id);
            var allImages = posts.GetAllImagesNamesByPostId(id);
            imageStorable.RemoveFileForPost(allImages.ToArray());
            posts.RemoveById(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
