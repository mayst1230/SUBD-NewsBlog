using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using NewsBlogBusinessLogic.DocumentModelsForTransfer;

namespace NewsBlogBusinessLogic.BusinessLogic
{
    public class DbTransferToMongo
    {
        static string connectionString = "mongodb://localhost:27017";
        static string DatabaseString = "newsblog";
        static string ArticlesCollectionString = "articles";
        static string UsersCollectionString = "users";

        public static async Task SaveArticle(ArticleDocumentModel model)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(DatabaseString);
            var collection = database.GetCollection<ArticleDocumentModel>(ArticlesCollectionString);
            await collection.InsertOneAsync(model);
        }
        public static async Task SaveUser(UserDocumentModel model)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(DatabaseString);
            var collection = database.GetCollection<UserDocumentModel>(UsersCollectionString);
            await collection.InsertOneAsync(model);
        }

        public static async Task<ObjectId> FindUser(UserDocumentModel model)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(DatabaseString);
            var collection = database.GetCollection<UserDocumentModel>(UsersCollectionString);
            var filter = new BsonDocument("nickname", model.NickName);
            var result = await collection.FindAsync(filter);
            return result.FirstOrDefault().Id;
        }
    }
}
