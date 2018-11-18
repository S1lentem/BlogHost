namespace Infrastructure.Models
{
    public class PostTagModel
    {
        public int PostId { get; set; }
        public virtual PostModel Post { get; set; }

        public int TagId { get; set; }
        public virtual TagModel Tag { get; set; }
    }
}
