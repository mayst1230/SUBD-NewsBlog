using MongoDB.Bson;

namespace NewsBlogBusinessLogic.DocumentModelsForTransfer
{
    public class Role
    { 
        public int NameRole { get; set; }
    }

    public class UserDocumentModel
    {
        public ObjectId Id { get; set; }
        public string NickName { get; set; }
        public Role Role { get; set; }
    }
}
