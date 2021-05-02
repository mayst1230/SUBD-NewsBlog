using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogBusinessLogic.Interfaces
{
    public interface IArticleStorage
    {
        List<ArticleViewModel> GetFullList();
        List<ArticleViewModel> GetFilteredList(ArticleBindingModel model);
        ArticleViewModel GetElement(ArticleBindingModel model);
        void Insert(ArticleBindingModel model);
        void Update(ArticleBindingModel model);
        void Delete(ArticleBindingModel model);
    }
}
