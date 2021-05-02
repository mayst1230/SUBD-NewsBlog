using System;
using System.Windows.Forms;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.BusinessLogic;
using Unity;

namespace NewsBlogView
{
    public partial class FormCategories : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly CategoryLogic logic;

        public FormCategories(CategoryLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormCategories_Load(object sender, EventArgs e)
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
                    dataGridViewCategories.DataSource = list;
                    dataGridViewCategories.AutoResizeColumns();
                    dataGridViewCategories.Columns[0].Visible = false;
                    dataGridViewCategories.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCategory>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            if (dataGridViewCategories.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormCategory>();
                form.Id = Convert.ToInt32(dataGridViewCategories.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewCategories.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridViewCategories.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        logic.Delete(new CategoryBindingModel { Id = id });
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
            if (string.IsNullOrEmpty(textBoxNametheme.Text))
            {
                MessageBox.Show("Строка поиска пуста", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var list = logic.Read(new CategoryBindingModel
                {
                    NameTheme = textBoxNametheme.Text
                });
                dataGridViewCategories.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
