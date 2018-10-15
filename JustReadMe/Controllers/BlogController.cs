using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using JustReadMe.ViewModels;
using JustReadMe.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace JustReadMe.Controllers
{
    public class BlogController : Controller
    {
        private BloghostContext db;

        public BlogController(BloghostContext context) => db = context;

        [Authorize]
        public IActionResult Index() =>
            View(db.Blogs.Where(model => model.UserModel.Email == User.Identity.Name));

        [HttpGet]
        [Authorize]
        public IActionResult CreateBlog() => View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBlog(CreateBlogModel model)
        {
            UserModel currentUser = await db.Users.FirstAsync(userModel => userModel.Email == User.Identity.Name);
            db.Blogs.Add(new BlogModel()
            {
                Title = model.Title,
                Description = model.Description,
                DateCreate = DateTime.Now,
                UserId = currentUser.Id,
                UserModel = currentUser
            });
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
