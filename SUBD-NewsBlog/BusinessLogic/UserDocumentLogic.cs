using System.Collections.Generic;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.Interfaces;
using NewsBlogBusinessLogic.ViewModels;

namespace NewsBlogBusinessLogic.BusinessLogic
{
    public class UserDocumentLogic
    {
        private readonly IUserDocumentStorageRedis userDocumentStorageRedis;
        private readonly IUserDocumentStorage userDocumentStorage;
        public UserDocumentLogic(IUserDocumentStorageRedis saleDocumentStorageRedis, IUserDocumentStorage saleDocumentStorage)
        {
            this.userDocumentStorageRedis = saleDocumentStorageRedis;
            this.userDocumentStorage = saleDocumentStorage;
        }
        public List<UserDocumentViewModel> Read(UserDocumentBindingModel model)
        {
            if (model == null)
            {
                var redisStorage = userDocumentStorageRedis.GetFullList();
                if (redisStorage != null && redisStorage.Count > 0)
                {
                    return redisStorage;
                }
                return userDocumentStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                var redisStorage = userDocumentStorageRedis.GetElement(model);
                if (redisStorage != null)
                {
                    return new List<UserDocumentViewModel> { redisStorage };
                }
                return new List<UserDocumentViewModel> { userDocumentStorage.GetElement(model) };
            }
            var redis = userDocumentStorageRedis.GetFilteredList(model);
            if (redis != null && redis.Count > 0)
            {
                return redis;
            }
            return userDocumentStorage.GetFilteredList(model);
        }
        public void UpdateCashe()
        {
            userDocumentStorageRedis.DeleteAll();
            var pgsql = userDocumentStorage.GetFullList();
            foreach (var user in pgsql)
            {
                userDocumentStorageRedis.InsertOrUpdate(new UserDocumentBindingModel
                {
                    Id = user.Id,
                    Nickname = user.Nickname,
                    NameRole = user.NameRole
                });
            }
        }
    }
}