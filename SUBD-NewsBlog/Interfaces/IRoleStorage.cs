using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogBusinessLogic.Interfaces
{
    public interface IRoleStorage
    {
        List<RoleViewModel> GetFullList();
        List<RoleViewModel> GetFilteredList(RoleBindingModel model);
        RoleViewModel GetElement(RoleBindingModel model);
        void Insert(RoleBindingModel model);
        void Update(RoleBindingModel model);
        void Delete(RoleBindingModel model);
    }
}
