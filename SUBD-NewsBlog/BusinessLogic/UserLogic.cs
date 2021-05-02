using System;
using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.Interfaces;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogBusinessLogic.BusinessLogic
{
    public class UserLogic
    {
        private readonly IUserStorage _userStorage;

        public UserLogic(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        public List<UserViewModel> Read(UserBindingModel model)
        {
            if (model == null)
            {
                return _userStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<UserViewModel> { _userStorage.GetElement(model) };
            }
            return _userStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(UserBindingModel model)
        {
            var user = _userStorage.GetElement(new UserBindingModel
            {
                Nickname = model.Nickname
            });
            if (user != null && user.Id != model.Id)
            {
                throw new Exception("Уже есть пользователь с таким никнеймом");
            }
            if (model.Id.HasValue)
            {
                _userStorage.Update(model);
            }
            else
            {
                _userStorage.Insert(model);
            }
        }

        public void Delete(UserBindingModel model)
        {
            var user = _userStorage.GetElement(new UserBindingModel
            {
                Id = model.Id
            });
            if (user == null)
            {
                throw new Exception("Никнейм не найден");
            }
            _userStorage.Delete(model);
        }
    }
}
