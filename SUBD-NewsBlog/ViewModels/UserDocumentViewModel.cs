using System;
using System.ComponentModel;
namespace NewsBlogBusinessLogic.ViewModels
{
    public class UserDocumentViewModel
    {
        public int Id { get; set; }
        [DisplayName("Никнейм пользователя")]
        public string Nickname { get; set; }
        [DisplayName("Роль пользователя")]
        public string NameRole { get; set; }
    }
}
