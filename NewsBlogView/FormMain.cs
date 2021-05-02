using System;
using Unity;
using System.Windows.Forms;

namespace NewsBlogView
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public FormMain()
        {
            InitializeComponent();
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
    }
}