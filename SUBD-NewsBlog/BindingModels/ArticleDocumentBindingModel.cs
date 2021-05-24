using System;
using System.Collections.Generic;
using System.Text;

namespace NewsBlogBusinessLogic.BindingModels
{
    public class ArticleDocumentBindingModel
    {
        public int? Id { get; set; }
        public string Category { get; set; }
        public string User { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DatePublish { get; set; }
        public int CommentUserId { get; set; }
        public string CommentUser { get; set; }
        public int CommentUserRole { get; set; }
        public string CommentUserText { get; set; }
        public DateTime CommentUserDateComment { get; set; }
    }
}
