using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustReadMe.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Message { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateChange { get; set; }

        public virtual BlogModel BlogModel { get; set; }
    }
}
