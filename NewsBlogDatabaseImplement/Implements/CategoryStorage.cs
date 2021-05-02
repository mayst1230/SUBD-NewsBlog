using System;
using System.Collections.Generic;
using System.Linq;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.Interfaces;
using NewsBlogBusinessLogic.ViewModels;
using NewsBlogDatabaseImplement.DatabaseContext;
using NewsBlogDatabaseImplement.Models;

namespace NewsBlogDatabaseImplement.Implements
{
    public class CategoryStorage : ICategoryStorage
    {
        public List<CategoryViewModel> GetFullList()
        {
            using (var context = new NewsBlogDatabase())
            {
                return context.Categories
                .Select(rec => new CategoryViewModel
                {
                    Id = rec.Idtheme,
                    NameTheme = rec.Nametheme
                }).ToList();
            }
        }
        public List<CategoryViewModel> GetFilteredList(CategoryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new NewsBlogDatabase())
            {
                return context.Categories
                .Where(rec => rec.Nametheme.Contains(model.NameTheme))
               .Select(rec => new CategoryViewModel
               {
                   Id = rec.Idtheme,
                   NameTheme = rec.Nametheme
               }).ToList();
            }
        }
        public CategoryViewModel GetElement(CategoryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new NewsBlogDatabase())
            {
                var category = context.Categories
                .FirstOrDefault(rec => rec.Nametheme.Equals(model.NameTheme) || rec.Idtheme == model.Id);
                return category != null ?
                new CategoryViewModel
                {
                    Id = category.Idtheme,
                    NameTheme = category.Nametheme
                } : null;
            }
        }
        public void Insert(CategoryBindingModel model)
        {
            using (var context = new NewsBlogDatabase())
            {
                context.Categories.Add(CreateModel(model, new Categories()));
                context.SaveChanges();
            }
        }
        public void Update(CategoryBindingModel model)
        {
            using (var context = new NewsBlogDatabase())
            {
                var category = context.Categories.FirstOrDefault(rec => rec.Idtheme == model.Id);
                if (category == null)
                {
                    throw new Exception("Категория не найдена");
                }
                CreateModel(model, category);
                context.SaveChanges();
            }
        }
        public void Delete(CategoryBindingModel model)
        {
            using (var context = new NewsBlogDatabase())
            {
                Categories category = context.Categories.FirstOrDefault(rec => rec.Idtheme == model.Id);
                if (category != null)
                {
                    context.Categories.Remove(category);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Категория не найдена");
                }
            }
        }
        private Categories CreateModel(CategoryBindingModel model, Categories category)
        {
            category.Nametheme = model.NameTheme;
            return category;
        }
    }
}
