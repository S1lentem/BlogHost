using System;

namespace Infrastructure.Models
{
    public class BlogModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreate { get; set; }

        public virtual UserModel UserModel { get; set; }
    }
}
