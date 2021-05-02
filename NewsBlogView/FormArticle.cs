using System;
using System.Windows.Forms;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.BusinessLogic;
using Unity;

namespace NewsBlogView
{
    public partial class FormArticle : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly ArticleLogic logicA;
        private readonly UserLogic logicU;
        private readonly CategoryLogic logicC;
        private int? id;

        public FormArticle(ArticleLogic logicA, UserLogic logicU, CategoryLogic logicC)
        {
            InitializeComponent();
            this.logicA = logicA;
            this.logicC = logicC;
            this.logicU = logicU;
        }

        private void FormArticle_Load(object sender, EventArgs e)
        {
            var listCategories = logicC.Read(null);
            comboBoxCategories.DataSource = listCategories;
            comboBoxCategories.ValueMember = "Id";
            comboBoxCategories.DisplayMember = "NameTheme";
            comboBoxCategories.SelectedItem = null;

            var listUsers = logicU.Read(null);
            comboBoxUsers.DataSource = listUsers;
            comboBoxUsers.ValueMember = "Id";
            comboBoxUsers.DisplayMember = "Nickname";
            comboBoxUsers.SelectedItem = null;
            if (id.HasValue)
            {
                try
                {
                    var view = logicA.Read(new ArticleBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        textBoxTitle.Text = view.Title;
                        textBoxText.Text = view.Text;
                        comboBoxCategories.SelectedValue = view.CategoryId;
                        comboBoxUsers.SelectedValue = view.UserId;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxText.Text))
            {
                MessageBox.Show("Заполните текст статьи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxTitle.Text))
            {
                MessageBox.Show("Заполните название статьи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxCategories.SelectedValue == null)
            {
                MessageBox.Show("Выберите категорию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxUsers.SelectedValue == null)
            {
                MessageBox.Show("Выберите пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                logicA.CreateOrUpdate(new ArticleBindingModel
                {
                    Id = id,
                    Title = textBoxTitle.Text,
                    Text = textBoxText.Text,
                    CategoryId = (int)comboBoxCategories.SelectedValue,
                    UserId = (int)comboBoxUsers.SelectedValue,
                    DateCreate = DateTime.Now
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}