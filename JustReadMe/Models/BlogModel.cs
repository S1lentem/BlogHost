using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustReadMe.Models
{
    public class BlogModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreate { get; set; }

        public int UserId { get; set; }
        public UserModel UserModel { get; set; }
    }
}
