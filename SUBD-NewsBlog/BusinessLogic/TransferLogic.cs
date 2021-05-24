using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.DocumentModelsForTransfer;
using NewsBlogBusinessLogic.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsBlogBusinessLogic.BusinessLogic
{
    public class TransferLogic
    {
        private readonly IUserStorage userStorage;
        private readonly IArticleStorage articleStorage;
        private readonly ICommentStorage commentStorage;
        private readonly IRoleStorage roleStorage;
        private readonly ICategoryStorage categoryStorage;

        public TransferLogic(IUserStorage userStorage, IArticleStorage articleStorage, ICommentStorage commentStorage, IRoleStorage roleStorage, ICategoryStorage categoryStorage)
        {
            this.userStorage = userStorage;
            this.articleStorage = articleStorage;
            this.commentStorage = commentStorage;
            this.roleStorage = roleStorage;
            this.categoryStorage = categoryStorage;
        }

        public async void TransferAll()
        {
            var articles = await Task.Run(() => articleStorage.GetFullList());
            var users = await Task.Run(() => userStorage.GetFullList());

            await Task.Run(async () =>
            {
                foreach (var user in users)
                {
                    await DbTransferToMongo.SaveUser(new UserDocumentModel
                    {
                        NickName = user.Nickname,
                        Role = new Role
                        {
                            NameRole = user.RoleId
                        }
                    });
                }
            });
        

        await Task.Run(async () =>
            {
                foreach (var article in articles)
                {
                    var category = await Task.Run(() => categoryStorage.GetElement(new CategoryBindingModel { Id = article.CategoryId }));
                    var user = await Task.Run(() => userStorage.GetElement(new UserBindingModel { Id = article.UserId }));
                    var comment = await Task.Run(() => commentStorage.GetFilteredList(new CommentBindingModel { ArticleId = article.Id }));
                    var commentDop = new List<Comments>();

                    foreach (var comments in comment)
                    {
                        var userDop = await Task.Run(() => userStorage.GetElement(new UserBindingModel { Id = comments.UserId }));
                        commentDop.Add(new Comments
                        {
                            User = userDop?.Nickname,
                            UserRole = (int)userDop?.RoleId,
                            ComentUser = comments.Comment,
                            DateCommentUser = comments.DateCreate
                        });
                    }
                    await DbTransferToMongo.SaveArticle(new ArticleDocumentModel
                    {
                        Text = article.Text,
                        Title = article.Title,
                        DatePublish = article.DateCreate,
                        Category = new Category
                        {
                            NameCategory = category.NameTheme,
                        },
                        User = new User
                        {
                            NickName = user.Nickname
                        },
                        Comment = commentDop
                    });
                }
            });
        }
    }
}