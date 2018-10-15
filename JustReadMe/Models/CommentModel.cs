using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
