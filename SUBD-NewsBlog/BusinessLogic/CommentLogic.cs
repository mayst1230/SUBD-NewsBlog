using System;
using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.Interfaces;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogBusinessLogic.BusinessLogic
{
    public class CommentLogic
    {
        private readonly ICommentStorage _commentStorage;

        public CommentLogic(ICommentStorage commentStorage)
        {
            _commentStorage = commentStorage;
        }

        public List<CommentViewModel> Read(CommentBindingModel model)
        {
            if (model == null)
            {
                return _commentStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<CommentViewModel> { _commentStorage.GetElement(model) };
            }
            return _commentStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(CommentBindingModel model)
        {
            var comment = _commentStorage.GetElement(new CommentBindingModel
            {
                Comment = model.Comment
            });
            if (comment != null && comment.Id != model.Id)
            {
                throw new Exception("Уже есть такой комментарий");
            }
            if (model.Id.HasValue)
            {
                _commentStorage.Update(model);
            }
            else
            {
                _commentStorage.Insert(model);
            }
        }

        public void Delete(CommentBindingModel model)
        {
            var comment = _commentStorage.GetElement(new CommentBindingModel
            {
                Id = model.Id
            });
            if (comment == null)
            {
                throw new Exception("Комментарий не найден");
            }
            _commentStorage.Delete(model);
        }
    }
}
