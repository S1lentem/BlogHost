using System;
using System.Collections.Generic;

namespace BlogHostCore.DomainModels
{
    public class Post
    {
        private string title;
        private string message;

        public int Id { get; }
        public DateTime DateCreation { get; }
        public DateTime? DateChange { get; private set; }
        public string AuthorName { get; }
        public List<Comment> Comments { get; }
        public List<string> Tags { get; }


        public Post(int id, string title, string message, DateTime dateCreation, 
            DateTime? dateChanges, string authorName, List<Comment> comments, List<string> tags)
        {
            this.Id = id;
            this.title = title;
            this.message = message;
            this.DateCreation = dateCreation;
            this.DateChange = dateChanges;
            this.AuthorName = authorName;
            this.Comments = comments;
            this.Tags = tags;
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
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
