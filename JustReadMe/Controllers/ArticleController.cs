using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using JustReadMe.Models;


namespace JustReadMe.Controllers
{
    public class ArticleController : Controller
    {
        private BloghostContext db;

        public ArticleController(BloghostContext context) => db = context;

        [Authorize]
        public IActionResult Index()
        {
            ViewBag.Blogs = db.Blogs.Where(model => model.UserModel.Email == User.Identity.Name);
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
