using System;
using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.Interfaces;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogBusinessLogic.BusinessLogic
{
    public class CategoryLogic
    {
        private readonly ICategoryStorage _categoryStorage;

        public CategoryLogic(ICategoryStorage categoryStorage)
        {
            _categoryStorage = categoryStorage;
        }

        public List<CategoryViewModel> Read(CategoryBindingModel model)
        {
            if (model == null)
            {
                return _categoryStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<CategoryViewModel> { _categoryStorage.GetElement(model) };
            }
            return _categoryStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(CategoryBindingModel model)
        {
            var category = _categoryStorage.GetElement(new CategoryBindingModel
            {
                NameTheme = model.NameTheme
            });
            if (category != null && category.Id != model.Id)
            {
                throw new Exception("Уже есть категория с таким названием");
            }
            if (model.Id.HasValue)
            {
                _categoryStorage.Update(model);
            }
            else
            {
                _categoryStorage.Insert(model);
            }
        }

        public void Delete(CategoryBindingModel model)
        {
            var category = _categoryStorage.GetElement(new CategoryBindingModel
            {
                Id = model.Id
            });
            if (category == null)
            {
                throw new Exception("Категория не найдена");
            }
            _categoryStorage.Delete(model);
        }
    }
}
