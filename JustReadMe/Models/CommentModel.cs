using System;

namespace JustReadMe.Models
{
    public class CommentModel
    {
        public string Message { get; set; }
        public DateTime DateCreate { get; }

        public int UserId { get; set; }
        public UserModel User { get; set; }

        public int ArticleId { get; set; }
        public BlogArticleModel Article { get; set; }
    }
}
