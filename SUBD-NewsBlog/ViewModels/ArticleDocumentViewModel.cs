using System;
using System.ComponentModel;

namespace NewsBlogBusinessLogic.ViewModels
{
    public class ArticleDocumentViewModel
    {
        public int? Id { get; set; }
        [DisplayName("Категория")]
        public string Category { get; set; }
        [DisplayName("Пользователь")]
        public string User { get; set; }
        [DisplayName("Заголовок")]
        public string Title { get; set; }
        [DisplayName("Текст")]
        public string Text { get; set; }
        [DisplayName("Дата публикации")]
        public DateTime DatePublish { get; set; }
        [DisplayName("Комментарий ID")]
        public int CommentUserId { get; set; }
        [DisplayName("Комментарий пользователя")]
        public string CommentUser { get; set; }
        [DisplayName("Роль пользователя")]
        public int CommentUserRole { get; set; }
        [DisplayName("Текст комментария пользователя")]
        public string CommentUserText { get; set; }
        [DisplayName("Дата публикации комментария")]
        public DateTime CommentUserDateComment { get; set; }
    }
}
