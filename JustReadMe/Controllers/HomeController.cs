using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using JustReadMe.Interfaces.Repository;

namespace JustReadMe.Controllers
{
    public class HomeController : Controller
    {
        private IBlogsRepository blogs;

        public HomeController(IBlogsRepository blogs) => this.blogs = blogs;

        [Authorize]
        public IActionResult Index() => View();

        public IActionResult NotLoginIndex() => View();

        public IActionResult SearchForm() => PartialView();

        public IActionResult Footer() => PartialView();
    }
}
