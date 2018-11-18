using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class TagModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public virtual ICollection<PostTagModel> PostTag { get; set; }

        public TagModel() => PostTag = new LinkedList<PostTagModel>();
    }
}
