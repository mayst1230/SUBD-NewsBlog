using System.ComponentModel;
using System;

namespace NewsBlogBusinessLogic.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }

        [DisplayName("Комментарий")]
        public string Comment { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
    }
}
