using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogBusinessLogic.Interfaces
{
    public interface IArticlelDocumentStorageRedis
    {
        ArticleDocumentViewModel GetElement(ArticleDocumentBindingModel model);
        List<ArticleDocumentViewModel> GetFilteredList(ArticleDocumentBindingModel model);
        List<ArticleDocumentViewModel> GetFullList();
        void InsertOrUpdate(ArticleDocumentBindingModel model);
        void DeleteAll();
    }
}
