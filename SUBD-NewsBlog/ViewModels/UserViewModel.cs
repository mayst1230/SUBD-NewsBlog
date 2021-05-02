using System.ComponentModel;

namespace NewsBlogBusinessLogic.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        [DisplayName("Никнейм")]
        public string Nickname { get; set; }
    }
}
