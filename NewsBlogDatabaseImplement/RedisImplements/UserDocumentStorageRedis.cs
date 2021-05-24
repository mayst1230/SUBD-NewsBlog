using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.Interfaces;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogDatabaseImplement.RedisImplements
{
    public class UserDocumentStorageRedis : IUserDocumentStorageRedis
    {
        public void DeleteAll()
        {
            using (var client = ConnectionMultiplexer.Connect("localhost"))
            {
                var db = client.GetDatabase();
                var users = db.SetMembers("Users");
                db.SetRemove("Users", users);
            }
        }

        public UserDocumentViewModel GetElement(UserDocumentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var client = ConnectionMultiplexer.Connect("localhost"))
            {
                var db = client.GetDatabase();
                var users = db.SetMembers("Users");
                foreach (var key in users)
                {
                    var user = JsonConvert.DeserializeObject<UserDocumentViewModel>(key);
                    if (user.Id == model.Id)
                    {
                        return user;
                    }
                }
            }
            return null;
        }

        public List<UserDocumentViewModel> GetFilteredList(UserDocumentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var list = new List<UserDocumentViewModel>();
            using (var client = ConnectionMultiplexer.Connect("localhost"))
            {
                var db = client.GetDatabase();
                var users = db.SetMembers("Users");
                foreach (var key in users)
                {
                    var user = JsonConvert.DeserializeObject<UserDocumentViewModel>(key);
                    if (user.Nickname == model.Nickname)
                    {
                        list.Add(user);
                    }
                }
            }
            return list;
        }

        public List<UserDocumentViewModel> GetFullList()
        {
            var list = new List<UserDocumentViewModel>();
            using (var client = ConnectionMultiplexer.Connect("localhost"))
            {
                var db = client.GetDatabase();
                var users = db.SetMembers("Users");
                foreach (var key in users)
                {
                    list.Add(CreateModel(key));
                }
            }
            return list;
        }

        public void InsertOrUpdate(UserDocumentBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            using (var client = ConnectionMultiplexer.Connect("localhost"))
            {
                var db = client.GetDatabase();
                db.SetAdd("Users", JsonConvert.SerializeObject(model));
            }
        }

        private UserDocumentViewModel CreateModel(string json)
        {
            if (json == null)
            {
                return null;
            }
            var result = JsonConvert.DeserializeObject<UserDocumentViewModel>(json);
            return result;
        }
    }
}
