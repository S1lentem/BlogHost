using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateChange { get; set; }

        public virtual BlogModel BlogModel { get; set; }
        public virtual ICollection<PostTagModel> PostTag { get; set; }

        public PostModel() => PostTag = new LinkedList<PostTagModel>();
    }
}
