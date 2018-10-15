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
        [Authorize]
        public IActionResult Index()
        {
            // Change!!!!!

            ViewBag.Blogs = new List<BlogModel> {
                new BlogModel() { Title="Title_1"},
                new BlogModel() { Title="Test_title",}
            };
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
