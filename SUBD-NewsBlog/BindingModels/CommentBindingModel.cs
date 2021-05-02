using System;

namespace NewsBlogBusinessLogic.BindingModels
{
    public class CommentBindingModel
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
