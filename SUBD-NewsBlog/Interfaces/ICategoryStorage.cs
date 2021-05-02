using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogBusinessLogic.Interfaces
{
    public interface ICategoryStorage
    {
        List<CategoryViewModel> GetFullList();
        List<CategoryViewModel> GetFilteredList(CategoryBindingModel model);
        CategoryViewModel GetElement(CategoryBindingModel model);
        void Insert(CategoryBindingModel model);
        void Update(CategoryBindingModel model);
        void Delete(CategoryBindingModel model);
    }
}
