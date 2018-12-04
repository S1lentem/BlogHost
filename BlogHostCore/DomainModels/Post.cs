using System;
using System.Collections.Generic;
using BlogHostCore.DomainModels.PostElements;

namespace BlogHostCore.DomainModels
{
    public class Post
    {
        private string title;

        public int Id { get; }
        public DateTime DateCreation { get; }
        public DateTime? DateChange { get; private set; }
        public string AuthorName { get; }
        public List<Comment> Comments { get; }
        public List<string> Tags { get; }
        public List<PostElement> Elements { get; }


        public Post(int id, string title, List<PostElement> elements, DateTime dateCreation, 
            DateTime? dateChanges, string authorName, List<Comment> comments, List<string> tags)
        {
            this.Id = id;
            this.title = title;
            this.Elements = elements;
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
    }
}
