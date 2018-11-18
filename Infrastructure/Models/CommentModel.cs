using System;

namespace Infrastructure.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        public string Message { get; set; }
        public DateTime DateCreate { get; set; }

        public virtual UserModel User { get; set; }
        public virtual PostModel Post { get; set; }
    }
}
