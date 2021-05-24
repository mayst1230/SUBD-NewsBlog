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
    public class CommentStorage : ICommentStorage
    {
        public List<CommentViewModel> GetFullList()
        {
            using (var context = new NewsBlogDatabase())
            {
                return context.Comments
                .Include(rec => rec.IdarticleNavigation)
                .Include(rec => rec.IduserNavigation)
                .Select(rec => new CommentViewModel
                {
                    Id = rec.Idcomment,
                    Comment = rec.Comment,
                    DateCreate = rec.Date,
                    ArticleId = rec.Idarticle,
                    UserId = rec.Iduser
                }).ToList();
            }
        }
        public List<CommentViewModel> GetFilteredList(CommentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new NewsBlogDatabase())
            {
                return context.Comments
                .Include(rec => rec.IdarticleNavigation)
                .Include(rec => rec.IduserNavigation)
                .Where(rec => rec.Comment.Contains(model.Comment) || (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.Date.Date == model.DateCreate.Date)
                || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.Date.Date >= model.DateFrom.Value.Date && rec.Date.Date <= model.DateTo.Value.Date) 
                || (model.ArticleId.HasValue && model.ArticleId.Value == rec.Idarticle))
                .Select(rec => new CommentViewModel
                {
                    Id = rec.Idcomment,
                    Comment = rec.Comment,
                    DateCreate = rec.Date,
                    ArticleId = rec.Idarticle,
                    UserId = rec.Iduser
                }).ToList();
            }
        }
        public CommentViewModel GetElement(CommentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new NewsBlogDatabase())
            {
                var comment = context.Comments
                    .Include(rec => rec.IdarticleNavigation)
                    .Include(rec => rec.IduserNavigation)
                    .FirstOrDefault(rec => rec.Comment.Equals(model.Comment) || rec.Idcomment == model.Id);
                return comment != null ?
                new CommentViewModel
                {
                    Id = comment.Idcomment,
                    Comment = comment.Comment,
                    DateCreate = comment.Date,
                    ArticleId = comment.Idarticle,
                    UserId = comment.Iduser
                } : null;
            }
        }
        public void Insert(CommentBindingModel model)
        {
            using (var context = new NewsBlogDatabase())
            {
                context.Comments.Add(CreateModel(model, new Comments()));
                context.SaveChanges();
            }
        }
        public void Update(CommentBindingModel model)
        {
            using (var context = new NewsBlogDatabase())
            {
                var comment = context.Comments.FirstOrDefault(rec => rec.Idcomment == model.Id);
                if (comment == null)
                {
                    throw new Exception("Комментарий не найден");
                }
                CreateModel(model, comment);
                context.SaveChanges();
            }
        }
        public void Delete(CommentBindingModel model)
        {
            using (var context = new NewsBlogDatabase())
            {
                Comments comment = context.Comments.FirstOrDefault(rec => rec.Idcomment == model.Id);
                if (comment != null)
                {
                    context.Comments.Remove(comment);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Комментарий не найден");
                }
            }
        }
        private Comments CreateModel(CommentBindingModel model, Comments comment)
        {
            comment.Comment = model.Comment;
            comment.Date = model.DateCreate;
            comment.Iduser = model.UserId;
            comment.Idarticle = (int)model.ArticleId;
            return comment;
        }
    }
}