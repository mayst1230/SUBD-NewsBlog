using Microsoft.EntityFrameworkCore;
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
    public class UserStorage : IUserStorage
    {
        public List<UserViewModel> GetFullList()
        {
            using (var context = new NewsBlogDatabase())
            {
                return context.Users
                .Include(rec => rec.IdroleNavigation)
                .Select(rec => new UserViewModel
                {
                    Id = rec.Iduser,
                    Nickname = rec.Nickname,
                    RoleId = rec.Idrole
                }).ToList();
            }
        }
        public List<UserViewModel> GetFilteredList(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new NewsBlogDatabase())
            {
                return context.Users
                .Include(rec => rec.IdroleNavigation)
                .Where(rec => rec.Nickname.Contains(model.Nickname))
                .Select(rec => new UserViewModel
                {
                    Id = rec.Iduser,
                    Nickname = rec.Nickname,
                    RoleId = rec.Idrole
                }).ToList();
            }
        }
        public UserViewModel GetElement(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new NewsBlogDatabase())
            {
                var user = context.Users
                    .Include(rec => rec.IdroleNavigation)
                    .FirstOrDefault(rec => rec.Nickname.Equals(model.Nickname) || rec.Iduser == model.Id);
                return user != null ?
                new UserViewModel
                {
                    Id = user.Iduser,
                    Nickname = user.Nickname,
                    RoleId = user.Idrole
                } : null;
            }
        }
        public void Insert(UserBindingModel model)
        {
            using (var context = new NewsBlogDatabase())
            {
                context.Users.Add(CreateModel(model, new Users()));
                context.SaveChanges();
            }
        }
        public void Update(UserBindingModel model)
        {
            using (var context = new NewsBlogDatabase())
            {
                var user = context.Users.FirstOrDefault(rec => rec.Iduser == model.Id);
                if (user == null)
                {
                    throw new Exception("Пользователь не найден");
                }
                CreateModel(model, user);
                context.SaveChanges();
            }
        }
        public void Delete(UserBindingModel model)
        {
            using (var context = new NewsBlogDatabase())
            {
                Users user = context.Users.FirstOrDefault(rec => rec.Iduser == model.Id);
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Пользователь не найден");
                }
            }
        }
        private Users CreateModel(UserBindingModel model, Users user)
        {
            user.Nickname = model.Nickname;
            user.Idrole = model.RoleId;
            return user;
        }
    }
}