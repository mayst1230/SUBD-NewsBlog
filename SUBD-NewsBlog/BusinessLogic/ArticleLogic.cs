using System;
using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.Interfaces;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogBusinessLogic.BusinessLogic
{
    public class ArticleLogic
    {
        private readonly IArticleStorage _articleStorage;

        public ArticleLogic(IArticleStorage articleStorage)
        {
            _articleStorage = articleStorage;
        }

        public List<ArticleViewModel> Read(ArticleBindingModel model)
        {
            if (model == null)
            {
                return _articleStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ArticleViewModel> { _articleStorage.GetElement(model) };
            }
            return _articleStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ArticleBindingModel model)
        {
            var article = _articleStorage.GetElement(new ArticleBindingModel
            {
                Title = model.Title
            });
            if (article != null && article.Id != model.Id)
            {
                throw new Exception("Уже есть статья с таким названием");
            }
            if (model.Id.HasValue)
            {
                _articleStorage.Update(model);
            }
            else
            {
                _articleStorage.Insert(model);
            }
        }

        public void Delete(ArticleBindingModel model)
        {
            var article = _articleStorage.GetElement(new ArticleBindingModel
            {
                Id = model.Id
            });
            if (article == null)
            {
                throw new Exception("Статья не найдена");
            }
            _articleStorage.Delete(model);
        }
    }
}
