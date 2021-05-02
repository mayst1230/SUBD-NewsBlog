using System;
using System.Windows.Forms;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.BusinessLogic;
using Unity;

namespace NewsBlogView
{
    public partial class FormArticlesSearch : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ArticleLogic logic;

        public FormArticlesSearch(ArticleLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormArticlesSearch_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
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

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxTitle.Text))
            {
                MessageBox.Show("Введите текст комментария",
               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var articles = logic.Read(new ArticleBindingModel
                {
                    Title = textBoxTitle.Text
                });
                dataGridViewArticles.DataSource = articles;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonSearchDate_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания",
               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var articles = logic.Read(new ArticleBindingModel
                {
                    DateFrom = dateTimePickerFrom.Value,
                    DateTo = dateTimePickerTo.Value,
                });
                dataGridViewArticles.DataSource = articles;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
    }
}
