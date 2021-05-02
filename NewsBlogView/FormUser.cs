using System;
using System.Windows.Forms;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.BusinessLogic;
using Unity;

namespace NewsBlogView
{
    public partial class FormUser : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly UserLogic logicU;
        private readonly RoleLogic logicR;
        private int? id;


        public FormUser(UserLogic logicU, RoleLogic logicR)
        {
            InitializeComponent();
            this.logicU = logicU;
            this.logicR = logicR;
        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            var listRoles = logicR.Read(null);
            comboBoxRoles.DataSource = listRoles;
            comboBoxRoles.ValueMember = "Id";
            comboBoxRoles.DisplayMember = "NameRole";
            comboBoxRoles.SelectedItem = null;
            if (id.HasValue)
            {
                try
                {
                    var view = logicU.Read(new UserBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        textBoxNickname.Text = view.Nickname;
                        comboBoxRoles.SelectedValue = view.RoleId;
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
            if (string.IsNullOrEmpty(textBoxNickname.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxRoles.SelectedValue == null)
            {
                MessageBox.Show("Выберите роль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                logicU.CreateOrUpdate(new UserBindingModel
                {
                    Id = id,
                    Nickname = textBoxNickname.Text,
                    RoleId = (int)comboBoxRoles.SelectedValue
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