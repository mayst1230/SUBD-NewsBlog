using System;
using System.Windows.Forms;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.BusinessLogic;
using Unity;

namespace NewsBlogView
{
    public partial class FormArticles : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ArticleLogic logic;

        public FormArticles(ArticleLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormArticles_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(null);
                if (list != null)
                {
                    dataGridViewArticles.DataSource = list;
                    dataGridViewArticles.AutoResizeColumns();
                    dataGridViewArticles.Columns[0].Visible = false;
                    dataGridViewArticles.Columns[1].Visible = false;
                    dataGridViewArticles.Columns[2].Visible = false;
                    dataGridViewArticles.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormArticle>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            if (dataGridViewArticles.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormArticle>();
                form.Id = Convert.ToInt32(dataGridViewArticles.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewArticles.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridViewArticles.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        logic.Delete(new ArticleBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormArticlesSearch>();
            form.ShowDialog();
        }
    }
}
