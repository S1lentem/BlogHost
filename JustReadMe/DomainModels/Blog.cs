using System;
using System.Collections.Generic;

namespace JustReadMe.DomainModels
{
    public class Blog
    {
        public int Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime DateCreation { get; }

        public Blog(int id, string title, string description, DateTime dateCreation)
        {
            Id = id;
            Title = title;
            Description = description;
            DateCreation = dateCreation;
        }
    }
}
