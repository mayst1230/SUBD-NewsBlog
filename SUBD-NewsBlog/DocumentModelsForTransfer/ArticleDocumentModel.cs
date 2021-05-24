using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace NewsBlogBusinessLogic.DocumentModelsForTransfer
{
    public class User
    {
        public string NickName { get; set; }
    }

    public class Category
    {
        public string NameCategory { get; set; }
    }

    public class Comments
    {
        public string User { get; set; }
        public int UserRole { get; set; }
        public string ComentUser { get; set; }
        public DateTime DateCommentUser { get; set; }
    }

    public class ArticleDocumentModel
    {
        public ObjectId Id { get; set; }
        public User User { get; set; }
        public List<Comments> Comment { get; set; }
        public Category Category { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public DateTime DatePublish { get; set; }
    }
}
