using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using JustReadMe.Models;
using JustReadMe.Interfaces.Repository;

namespace JustReadMe.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleRepository articles;
        private readonly IBlogsRepository blogs;

        public ArticleController(IArticleRepository articles, IBlogsRepository blogs)
        {
            this.articles = articles;
            this.blogs = blogs;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.blogs = await blogs.GetAll(model => model.UserModel.Email == User.Identity.Name);
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index((string message, string tag, BlogModel blog) info)
        {
            articles.CreatePost((info.message, info.tag), await blogs.Find(model => model.UserModel.Email == User.Identity.Name));
            ViewBag.blogs = await blogs.GetAll(model => model.UserModel.Email == User.Identity.Name);
            return View();
        }

        [HttpGet]
        public IActionResult SearchByTag() => View();

        [HttpPost]
        public IActionResult SearchByTag(string tag)
        {
            ViewBag.Authorized = User.Identity.IsAuthenticated;
            ViewBag.Tag = tag;
            return View();
        }

        [HttpGet]
        public IActionResult PostArticle() => PartialView();   
    }
}
