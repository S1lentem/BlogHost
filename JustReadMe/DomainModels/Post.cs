using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustReadMe.DomainModels
{
    public class Post
    {
        private string tag;
        private string message;

        public int Id { get; }
        public DateTime DateCreation { get; }
        public DateTime? DateChange { get; private set; }
        public string AuthorName { get; }
        public List<Comment> Comments { get; }

        public Post(int id, string tag, string message, DateTime dateCreation, 
            DateTime? dateChanges, string authorName, List<Comment> comments)
        {
            this.Id = id;
            this.tag = tag;
            this.message = message;
            this.DateCreation = dateCreation;
            this.DateChange = dateChanges;
            this.AuthorName = authorName;
            this.Comments = comments;
        }

        public string Tag
        {
            get => tag;
            set
            {
                tag = value;
                DateChange = DateTime.Now;
            }
        }
        public string Message


        {
            get => message;
            set
            {
                message = value;
                DateChange = DateTime.Now;
            }
        }

    }
}
