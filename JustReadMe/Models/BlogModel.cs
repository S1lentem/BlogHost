using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustReadMe.Models
{
    public class BlogModel : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreate { get; set; }

        public virtual UserModel UserModel { get; set; }
    }
}
