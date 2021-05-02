using System;
using System.Windows.Forms;
using NewsBlogBusinessLogic.BindingModels;
using NewsBlogBusinessLogic.BusinessLogic;
using Unity;

namespace NewsBlogView
{
    public partial class FormCommentsSearch : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly CommentLogic logic;

        public FormCommentsSearch(CommentLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormCommentsSearch_Load(object sender, EventArgs e)
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
                    dataGridViewComments.DataSource = list;
                    dataGridViewComments.AutoResizeColumns();
                    dataGridViewComments.Columns[0].Visible = false;
                    dataGridViewComments.Columns[1].Visible = false;
                    dataGridViewComments.Columns[2].Visible = false;
                    dataGridViewComments.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxComment.Text))
            {
                MessageBox.Show("Введите текст комментария",
               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var comments = logic.Read(new CommentBindingModel
                {
                    Comment = textBoxComment.Text
                });
                dataGridViewComments.DataSource = comments;
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
                var comments = logic.Read(new CommentBindingModel
                {
                    DateFrom = dateTimePickerFrom.Value,
                    DateTo = dateTimePickerTo.Value,
                });
                dataGridViewComments.DataSource = comments;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
    }
}
