using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHostCore.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly ICommentRepository commentRepository;

        public CommentsController(ICommentRepository commentRepository)
            => this.commentRepository = commentRepository;


        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id) => new JsonResult(commentRepository.GetByPostId(id));
        

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post(int id, string message) 
            => new JsonResult(commentRepository.SendComment(message, User.Identity.Name, id));
        

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
           // commentRepository.RemoveById(id);
        }
    }
}
