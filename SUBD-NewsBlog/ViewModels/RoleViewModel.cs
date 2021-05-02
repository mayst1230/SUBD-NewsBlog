using System.ComponentModel;

namespace NewsBlogBusinessLogic.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название роли")]
        public string NameRole { get; set; }
    }
}
