using System;
using System.Windows.Forms;
using NewsBlogBusinessLogic.BusinessLogic;
using NewsBlogBusinessLogic.Interfaces;
using NewsBlogDatabaseImplement.Implements;
using Unity;
using Unity.Lifetime;

namespace NewsBlogView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IArticleStorage, ArticleStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICategoryStorage, CategoryStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICommentStorage, CommentStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRoleStorage, RoleStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IUserStorage, UserStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ArticleLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<CategoryLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<CommentLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<RoleLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<UserLogic>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}