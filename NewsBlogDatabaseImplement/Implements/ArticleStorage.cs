using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.Interfaces;
using NewsBlogBusinessLogic.ViewModels;
using NewsBlogDatabaseImplement.DatabaseContext;
using NewsBlogDatabaseImplement.Models;

namespace NewsBlogDatabaseImplement.Implements
{
    public class ArticleStorage : IArticleStorage
    {
        public List<ArticleViewModel> GetFullList()
        {
            using (var context = new NewsBlogDatabase())
            {
                return context.Articles
                .Include(rec => rec.IdthemeNavigation)
                .Include(rec => rec.IduserNavigation)
                .Select(rec => new ArticleViewModel
                {
                    Id = rec.Idarticle,
                    Title = rec.Title,
                    Text = rec.Text,
                    DateCreate = rec.Date,
                    CategoryId = rec.Idtheme,
                    UserId = rec.Iduser
                }).ToList();
            }
        }
        public List<ArticleViewModel> GetFilteredList(ArticleBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new NewsBlogDatabase())
            {
                return context.Articles
                .Include(rec => rec.IdthemeNavigation)
                .Include(rec => rec.IduserNavigation)
                .Where(rec => rec.Title.Contains(model.Title) || (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.Date.Date == model.DateCreate.Date)
                || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.Date.Date >= model.DateFrom.Value.Date && rec.Date.Date <= model.DateTo.Value.Date))
                .Select(rec => new ArticleViewModel
                {
                    Id = rec.Idarticle,
                    Title = rec.Title,
                    Text = rec.Text,
                    DateCreate = rec.Date,
                    CategoryId = rec.Idtheme,
                    UserId = rec.Iduser
                }).ToList();
            }
        }
        public ArticleViewModel GetElement(ArticleBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new NewsBlogDatabase())
            {
                var article = context.Articles
                    .Include(rec => rec.IdthemeNavigation)
                    .Include(rec => rec.IduserNavigation)
                    .FirstOrDefault(rec => rec.Title.Equals(model.Title) || rec.Idarticle == model.Id);
                return article != null ?
                new ArticleViewModel
                {
                    Id = article.Idarticle,
                    Title = article.Title,
                    Text = article.Text,
                    DateCreate = article.Date,
                    CategoryId = article.Idtheme,
                    UserId = article.Iduser
                } : null;
            }
        }
        public void Insert(ArticleBindingModel model)
        {
            using (var context = new NewsBlogDatabase())
            {
                context.Articles.Add(CreateModel(model, new Articles()));
                context.SaveChanges();
            }
        }
        public void Update(ArticleBindingModel model)
        {
            using (var context = new NewsBlogDatabase())
            {
                var article = context.Articles.FirstOrDefault(rec => rec.Idarticle == model.Id);
                if (article == null)
                {
                    throw new Exception("Статья не найдена");
                }
                CreateModel(model, article);
                context.SaveChanges();
            }
        }
        public void Delete(ArticleBindingModel model)
        {
            using (var context = new NewsBlogDatabase())
            {
                Articles article = context.Articles.FirstOrDefault(rec => rec.Idarticle == model.Id);
                if (article != null)
                {
                    context.Articles.Remove(article);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Статья не найдена");
                }
            }
        }
        private Articles CreateModel(ArticleBindingModel model, Articles article)
        {
            article.Title = model.Title;
            article.Text = model.Text;
            article.Date = model.DateCreate;
            article.Iduser = model.UserId;
            article.Idtheme = model.CategoryId;
            return article;
        }
    }
}
