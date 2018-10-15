    using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using JustReadMe.Models;

namespace JustReadMe.Controllers
{
    public class HomeController : Controller
    {
        private BloghostContext db;

        public HomeController(BloghostContext context) => db = context;

        [Authorize]
        public IActionResult Index() => View();

        public IActionResult NotLoginIndex() => View();

        public IActionResult SearchForm() => PartialView();

        public IActionResult Footer() => PartialView();
    }
}
