using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.Interfaces;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogBusinessLogic.BusinessLogic
{
    public class ArticleDocumentLogic
    {
        private readonly IArticleDocumentStorage articleDocumentStorage;
        private readonly IArticlelDocumentStorageRedis articleDocumentStorageRedis;

        public ArticleDocumentLogic(IArticlelDocumentStorageRedis articleDocumentStorageRedis, IArticleDocumentStorage articleDocumentStorage)
        {
            this.articleDocumentStorageRedis = articleDocumentStorageRedis;
            this.articleDocumentStorage = articleDocumentStorage;
        }

        public List<ArticleDocumentViewModel> Read(ArticleDocumentBindingModel model)
        {
            if (model == null)
            {
                var redisStorage = articleDocumentStorageRedis.GetFullList();
                if (redisStorage != null && redisStorage.Count > 0)
                {
                    return redisStorage;
                }
                return articleDocumentStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                var redisStorage = articleDocumentStorageRedis.GetElement(model);
                if (redisStorage != null)
                {
                    return new List<ArticleDocumentViewModel> { redisStorage };
                }
                return new List<ArticleDocumentViewModel> { articleDocumentStorage.GetElement(model) };
            }
            var redis = articleDocumentStorageRedis.GetFilteredList(model);
            if (redis != null && redis.Count > 0)
            {
                return redis;
            }
            return articleDocumentStorage.GetFilteredList(model);
        }
        public void UpdateCashe()
        {
            articleDocumentStorageRedis.DeleteAll();
            var pgsql = articleDocumentStorage.GetFullList();
            foreach (var article in pgsql)
            {
                articleDocumentStorageRedis.InsertOrUpdate(new ArticleDocumentBindingModel
                {
                    Id = article.Id,
                    Category = article.Category,
                    User = article.User,
                    Title = article.Title,
                    Text = article.Text,
                    DatePublish = article.DatePublish,
                    CommentUserId = article.CommentUserId,
                    CommentUser = article.CommentUser,
                    CommentUserRole = article.CommentUserRole,
                    CommentUserText = article.CommentUserText,
                    CommentUserDateComment = article.CommentUserDateComment
                });
            }
        }

    }
}
