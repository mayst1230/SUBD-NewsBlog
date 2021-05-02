using System.ComponentModel;

namespace NewsBlogBusinessLogic.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [DisplayName("Тема")]
        public string NameTheme { get; set; }
    }
}
