using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogBusinessLogic.Interfaces
{
    public interface IUserDocumentStorage
    {
        UserDocumentViewModel GetElement(UserDocumentBindingModel model);
        List<UserDocumentViewModel> GetFilteredList(UserDocumentBindingModel model);
        List<UserDocumentViewModel> GetFullList();
    }
}
