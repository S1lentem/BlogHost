using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using JustReadMe.Interfaces.Repository;

namespace JustReadMe.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogsRepository blogs;

        public HomeController(IBlogsRepository blogs) => this.blogs = blogs;

        public IActionResult Index() => View();

        public IActionResult SearchForm() => PartialView();

        public IActionResult Footer() => PartialView();
    }
}
