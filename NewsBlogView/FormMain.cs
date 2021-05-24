using System;
using Unity;
using System.Windows.Forms;
using NewsBlogBusinessLogic.BusinessLogic;

namespace NewsBlogView
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly TransferLogic logicT;

        public FormMain(TransferLogic logicT)
        {
            InitializeComponent();
            this.logicT = logicT;
        }

        private void buttonRoles_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormRoles>();
            form.ShowDialog();
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormUsers>();
            form.ShowDialog();
        }

        private void buttonCategories_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCategories>();
            form.ShowDialog();
        }

        private void buttonArticles_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormArticles>();
            form.ShowDialog();
        }

        private void buttonComments_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormComments>();
            form.ShowDialog();
        }

        private void buttonImportData_Click(object sender, EventArgs e)
        {
            logicT.TransferAll();
        }

        private void buttonUserDocument_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormUserDocuments>();
            form.ShowDialog();
        }

        private void buttonArticleDocument_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormArticleDocuments>();
            form.ShowDialog();
        }
    }
}