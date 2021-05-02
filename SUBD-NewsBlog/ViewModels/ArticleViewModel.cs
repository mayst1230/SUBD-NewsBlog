using System.ComponentModel;
using System;

namespace NewsBlogBusinessLogic.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }

        [DisplayName("Название")]
        public string Title { get; set; }

        [DisplayName("Текст")]
        public string Text { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
    }
}
