using System;
using System.Windows.Forms;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.BusinessLogic;
using Unity;

namespace NewsBlogView
{
    public partial class FormComment : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly ArticleLogic logicA;
        private readonly UserLogic logicU;
        private readonly CommentLogic logicC;
        private int? id;

        public FormComment(ArticleLogic logicA, UserLogic logicU, CommentLogic logicC)
        {
            InitializeComponent();
            this.logicA = logicA;
            this.logicC = logicC;
            this.logicU = logicU;
        }

        private void FormComment_Load(object sender, EventArgs e)
        {
            var listArticles = logicA.Read(null);
            comboBoxArticles.DataSource = listArticles;
            comboBoxArticles.ValueMember = "Id";
            comboBoxArticles.DisplayMember = "Title";
            comboBoxArticles.SelectedItem = null;

            var listUsers = logicU.Read(null);
            comboBoxUsers.DataSource = listUsers;
            comboBoxUsers.ValueMember = "Id";
            comboBoxUsers.DisplayMember = "Nickname";
            comboBoxUsers.SelectedItem = null;
            if (id.HasValue)
            {
                try
                {
                    var view = logicC.Read(new CommentBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        textBoxComment.Text = view.Comment;
                        comboBoxArticles.SelectedValue = view.ArticleId;
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
            if (string.IsNullOrEmpty(textBoxComment.Text))
            {
                MessageBox.Show("Заполните комментарий к статье", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxArticles.SelectedValue == null)
            {
                MessageBox.Show("Выберите статью", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxUsers.SelectedValue == null)
            {
                MessageBox.Show("Выберите пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                logicC.CreateOrUpdate(new CommentBindingModel
                {
                    Id = id,
                    Comment = textBoxComment.Text,
                    ArticleId = (int)comboBoxArticles.SelectedValue,
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
