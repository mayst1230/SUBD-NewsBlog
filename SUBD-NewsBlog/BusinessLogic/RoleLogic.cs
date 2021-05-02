using System;
using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.Interfaces;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogBusinessLogic.BusinessLogic
{
    public class RoleLogic
    {
        private readonly IRoleStorage _roleStorage;

        public RoleLogic(IRoleStorage roleStorage)
        {
            _roleStorage = roleStorage;
        }

        public List<RoleViewModel> Read(RoleBindingModel model)
        {
            if (model == null)
            {
                return _roleStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<RoleViewModel> { _roleStorage.GetElement(model) };
            }
            return _roleStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(RoleBindingModel model)
        {
            var role = _roleStorage.GetElement(new RoleBindingModel
            {
                NameRole = model.NameRole
            });
            if (role != null && role.Id != model.Id)
            {
                throw new Exception("Уже есть роль с таким названием");
            }
            if (model.Id.HasValue)
            {
                _roleStorage.Update(model);
            }
            else
            {
                _roleStorage.Insert(model);
            }
        }

        public void Delete(RoleBindingModel model)
        {
            var role = _roleStorage.GetElement(new RoleBindingModel
            {
                Id = model.Id
            });
            if (role == null)
            {
                throw new Exception("Роль не найдена");
            }
            _roleStorage.Delete(model);
        }
    }
}
