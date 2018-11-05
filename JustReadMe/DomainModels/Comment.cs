﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustReadMe.DomainModels
{
    public class Comment
    {
        public int Id { get; }
        public string UserName { get; }
        public int PostId { get; }
        public DateTime DateCreation { get; }
        public string Message { get; }

        public Comment(int id, string userName, int postId, DateTime dateCreation, string message)
        {
            Id = id;
            UserName = userName;
            PostId = PostId;
            DateCreation = dateCreation;
            Message = message;
        }
    }
}
