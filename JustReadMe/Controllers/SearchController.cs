using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHostCore.DomainModels;
using BlogHostCore.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly IPostRepository postRepository;

        public SearchController(IPostRepository postRepository) => this.postRepository = postRepository;

        
        [HttpGet]
        public IActionResult Get() =>  new JsonResult(postRepository.GetDescriptionAllByUserName(User.Identity.Name));

        
        [HttpGet("{title}")]
        public IActionResult Get(string title)
        {
            var result = postRepository.GetDescriptionAllPostContainsInTitle(title, User.Identity.Name);
            return new JsonResult(result);
        }
    }
}
