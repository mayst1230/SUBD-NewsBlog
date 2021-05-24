using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.Interfaces;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogDatabaseImplement.RedisImplements
{
    public class ArticleDocumentStorageRedis : IArticlelDocumentStorageRedis
    {
        public void DeleteAll()
        {
            using (var client = ConnectionMultiplexer.Connect("localhost"))
            {
                var db = client.GetDatabase();
                var articles = db.SetMembers("Articles");
                db.SetRemove("Articles", articles);
            }
        }

        public ArticleDocumentViewModel GetElement(ArticleDocumentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var client = ConnectionMultiplexer.Connect("localhost"))
            {
                var db = client.GetDatabase();
                var articles = db.SetMembers("Articles");
                foreach (var key in articles)
                {
                    var article = JsonConvert.DeserializeObject<ArticleDocumentViewModel>(key);
                    if (article.Id == model.Id)
                    {
                        return article;
                    }
                }
            }
            return null;
        }

        public List<ArticleDocumentViewModel> GetFilteredList(ArticleDocumentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var list = new List<ArticleDocumentViewModel>();
            using (var client = ConnectionMultiplexer.Connect("localhost"))
            {
                var db = client.GetDatabase();
                var articles = db.SetMembers("Articles");
                foreach (var key in articles)
                {
                    var article = JsonConvert.DeserializeObject<ArticleDocumentViewModel>(key);
                    if (article.Text == model.Text)
                    {
                        list.Add(article);
                    }
                }
            }
            return list;
        }

        public List<ArticleDocumentViewModel> GetFullList()
        {
            var list = new List<ArticleDocumentViewModel>();
            using (var client = ConnectionMultiplexer.Connect("localhost"))
            {
                var db = client.GetDatabase();
                var articles = db.SetMembers("Articles");
                foreach (var key in articles)
                {
                    list.Add(CreateModel(key));
                }
            }
            return list;
        }

        public void InsertOrUpdate(ArticleDocumentBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            using (var client = ConnectionMultiplexer.Connect("localhost"))
            {
                var db = client.GetDatabase();
                db.SetAdd("Articles", JsonConvert.SerializeObject(model));
            }
        }

        private ArticleDocumentViewModel CreateModel(string json)
        {
            if (json == null)
            {
                return null;
            }
            var result = JsonConvert.DeserializeObject<ArticleDocumentViewModel>(json);
            return result;
        }
    }
}
