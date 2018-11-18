using BlogHostCore.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
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
