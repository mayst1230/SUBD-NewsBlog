using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.Interfaces;
using NewsBlogBusinessLogic.ViewModels;
using NewsBlogDatabaseImplement.DatabaseContext;
using System;

namespace NewsBlogDatabaseImplement.Implements
{
    public class UserDocumentStorage : IUserDocumentStorage
    {
        public UserDocumentViewModel GetElement(UserDocumentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new NewsBlogDatabase())
            {
                var user = context.Users
                .Include(rec => rec.IdroleNavigation)
                .FirstOrDefault(rec => rec.Iduser == model.Id);
                return user != null ?
                new UserDocumentViewModel
                {
                    Id = user.Iduser,
                    Nickname = user.Nickname,
                    NameRole = user.IdroleNavigation.Namerole
                } : null;
            }
        }

        public List<UserDocumentViewModel> GetFilteredList(UserDocumentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new NewsBlogDatabase())
            {
                return context.Users
                .Include(rec => rec.IdroleNavigation)
                .Where(rec => rec.Nickname == model.Nickname)
                .Select(rec => new UserDocumentViewModel
                {
                    Id = rec.Iduser,
                    Nickname = rec.Nickname,
                    NameRole = rec.IdroleNavigation.Namerole
                }).ToList();
            }
        }

        public List<UserDocumentViewModel> GetFullList()
        {
            using (var context = new NewsBlogDatabase())
            {
                return context.Users
                .Include(rec => rec.IdroleNavigation)
                .Select(rec => new UserDocumentViewModel
                {
                    Id = rec.Iduser,
                    Nickname = rec.Nickname,
                    NameRole = rec.IdroleNavigation.Namerole
                }).ToList();
            }
        }
    }
}
