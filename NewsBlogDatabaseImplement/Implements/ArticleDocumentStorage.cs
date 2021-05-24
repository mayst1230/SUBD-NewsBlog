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
    public class ArticleDocumentStorage : IArticleDocumentStorage
    {
        public ArticleDocumentViewModel GetElement(ArticleDocumentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new NewsBlogDatabase())
            {
                var article = context.Articles
                .Include(rec => rec.IduserNavigation)
                .Include(rec => rec.IdthemeNavigation)
                .Include(rec => rec.Comments)
                .FirstOrDefault(rec => rec.Idarticle == model.Id);
                return article != null ?
                new ArticleDocumentViewModel
                {
                    Id = article.Idarticle,
                    Category = article.IdthemeNavigation.Nametheme,
                    Title = article.Title,
                    Text = article.Text,
                    User = article.IduserNavigation.Nickname,
                    DatePublish = article.Date,
                    CommentUser = Convert.ToString(article.IduserNavigation.Comments),
                    CommentUserDateComment = article.Date,
                    CommentUserId = article.Iduser,
                    CommentUserText = Convert.ToString(article.IduserNavigation.Articles),
                    CommentUserRole = article.IduserNavigation.Idrole
                } : null;
            }
        }

        public List<ArticleDocumentViewModel> GetFilteredList(ArticleDocumentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new NewsBlogDatabase())
            {
                return context.Articles
                .Include(rec => rec.IduserNavigation)
                .Include(rec => rec.IdthemeNavigation)
                .Include(rec => rec.Comments)
                .Where(rec => rec.Title == model.Title)
                .Select(rec => new ArticleDocumentViewModel
                {
                    Id = rec.Idarticle,
                    Category = rec.IdthemeNavigation.Nametheme,
                    Title = rec.Title,
                    Text = rec.Text,
                    User = rec.IduserNavigation.Nickname,
                    DatePublish = rec.Date,
                    CommentUser = Convert.ToString(rec.IduserNavigation.Comments),
                    CommentUserDateComment = rec.Date,
                    CommentUserId = rec.Iduser,
                    CommentUserText = Convert.ToString(rec.IduserNavigation.Articles),
                    CommentUserRole = rec.IduserNavigation.Idrole
                }).ToList();
            }
        }

        public List<ArticleDocumentViewModel> GetFullList()
        {
            using (var context = new NewsBlogDatabase())
            {
                return context.Articles
                .Include(rec => rec.IduserNavigation)
                .Include(rec => rec.IdthemeNavigation)
                .Include(rec => rec.Comments)
                .Select(rec => new ArticleDocumentViewModel
                {
                    Id = rec.Idarticle,
                    Category = rec.IdthemeNavigation.Nametheme,
                    Title = rec.Title,
                    Text = rec.Text,
                    User = rec.IduserNavigation.Nickname,
                    DatePublish = rec.Date,
                    CommentUser = Convert.ToString(rec.IduserNavigation.Comments),
                    CommentUserDateComment = rec.Date,
                    CommentUserId = rec.Iduser,
                    CommentUserText = Convert.ToString(rec.IduserNavigation.Articles),
                    CommentUserRole = rec.IduserNavigation.Idrole
                }).ToList();
            }
        }
    }
}
