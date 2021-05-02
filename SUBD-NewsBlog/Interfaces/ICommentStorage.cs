using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogBusinessLogic.Interfaces
{
    public interface ICommentStorage
    {
        List<CommentViewModel> GetFullList();
        List<CommentViewModel> GetFilteredList(CommentBindingModel model);
        CommentViewModel GetElement(CommentBindingModel model);
        void Insert(CommentBindingModel model);
        void Update(CommentBindingModel model);
        void Delete(CommentBindingModel model);
    }
}
