using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogBusinessLogic.Interfaces
{
    public interface IUserStorage
    {
        List<UserViewModel> GetFullList();
        List<UserViewModel> GetFilteredList(UserBindingModel model);
        UserViewModel GetElement(UserBindingModel model);
        void Insert(UserBindingModel model);
        void Update(UserBindingModel model);
        void Delete(UserBindingModel model);
    }
}
