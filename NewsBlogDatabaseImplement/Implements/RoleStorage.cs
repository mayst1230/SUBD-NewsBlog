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
    public class RoleStorage : IRoleStorage
    {
        public List<RoleViewModel> GetFullList()
        {
            using (var context = new NewsBlogDatabase())
            {
                return context.Role
                .Select(rec => new RoleViewModel
                {
                    Id = rec.Idrole,
                    NameRole = rec.Namerole
                }).ToList();
            }
        }
        public List<RoleViewModel> GetFilteredList(RoleBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new NewsBlogDatabase())
            {
                return context.Role
               .Where(rec => rec.Namerole.Contains(model.NameRole))
               .Select(rec => new RoleViewModel
               {
                   Id = rec.Idrole,
                   NameRole = rec.Namerole
               }).ToList();
            }
        }
        public RoleViewModel GetElement(RoleBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new NewsBlogDatabase())
            {
                var role = context.Role
                .FirstOrDefault(rec => rec.Namerole.Equals(model.NameRole) || rec.Idrole == model.Id);
                return role != null ?
                new RoleViewModel
                {
                    Id = role.Idrole,
                    NameRole = role.Namerole
                } : null;
            }
        }
        public void Insert(RoleBindingModel model)
        {
            using (var context = new NewsBlogDatabase())
            {
                context.Role.Add(CreateModel(model, new Role()));
                context.SaveChanges();
            }
        }
        public void Update(RoleBindingModel model)
        {
            using (var context = new NewsBlogDatabase())
            {
                var role = context.Role.FirstOrDefault(rec => rec.Idrole == model.Id);
                if (role == null)
                {
                    throw new Exception("Роль не найдена");
                }
                CreateModel(model, role);
                context.SaveChanges();
            }
        }
        public void Delete(RoleBindingModel model)
        {
            using (var context = new NewsBlogDatabase())
            {
                Role role = context.Role.FirstOrDefault(rec => rec.Idrole == model.Id);
                if (role != null)
                {
                    context.Role.Remove(role);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Роль не найдена");
                }
            }
        }
        private Role CreateModel(RoleBindingModel model, Role role)
        {
            role.Namerole= model.NameRole;
            return role;
        }
    }
}