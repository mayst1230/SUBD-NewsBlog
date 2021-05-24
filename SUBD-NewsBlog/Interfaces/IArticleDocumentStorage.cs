using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogBusinessLogic.Interfaces
{
    public interface IArticleDocumentStorage
    {
        ArticleDocumentViewModel GetElement(ArticleDocumentBindingModel model);
        List<ArticleDocumentViewModel> GetFilteredList(ArticleDocumentBindingModel model);
        List<ArticleDocumentViewModel> GetFullList();
    }
}
