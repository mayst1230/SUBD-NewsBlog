using System;

namespace NewsBlogBusinessLogic.BindingModels
{
    public class ArticleBindingModel
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateCreate { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
