using System;
using System.Windows.Forms;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.BusinessLogic;
using Unity;

namespace NewsBlogView
{
    public partial class FormArticleDocuments : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ArticleDocumentLogic logic;

        public FormArticleDocuments(ArticleDocumentLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormArticleDocuments_Load(object sender, EventArgs e)
        {
            LoadData(null);
        }

        private void LoadData(ArticleDocumentBindingModel model)
        {
            try
            {
                var list = logic.Read(model);
                if (list != null)
                {
                    dataGridViewArticleDocuments.DataSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            LoadData(null);
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            logic.UpdateCashe();
        }
    }
}
