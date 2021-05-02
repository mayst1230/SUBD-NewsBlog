namespace NewsBlogView
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRoles = new System.Windows.Forms.Button();
            this.buttonUsers = new System.Windows.Forms.Button();
            this.buttonCategories = new System.Windows.Forms.Button();
            this.buttonArticles = new System.Windows.Forms.Button();
            this.buttonComments = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRoles
            // 
            this.buttonRoles.Location = new System.Drawing.Point(68, 12);
            this.buttonRoles.Name = "buttonRoles";
            this.buttonRoles.Size = new System.Drawing.Size(179, 23);
            this.buttonRoles.TabIndex = 0;
            this.buttonRoles.Text = "Роли";
            this.buttonRoles.UseVisualStyleBackColor = true;
            this.buttonRoles.Click += new System.EventHandler(this.buttonRoles_Click);
            // 
            // buttonUsers
            // 
            this.buttonUsers.Location = new System.Drawing.Point(68, 41);
            this.buttonUsers.Name = "buttonUsers";
            this.buttonUsers.Size = new System.Drawing.Size(179, 23);
            this.buttonUsers.TabIndex = 1;
            this.buttonUsers.Text = "Пользователи";
            this.buttonUsers.UseVisualStyleBackColor = true;
            this.buttonUsers.Click += new System.EventHandler(this.buttonUsers_Click);
            // 
            // buttonCategories
            // 
            this.buttonCategories.Location = new System.Drawing.Point(68, 70);
            this.buttonCategories.Name = "buttonCategories";
            this.buttonCategories.Size = new System.Drawing.Size(179, 23);
            this.buttonCategories.TabIndex = 2;
            this.buttonCategories.Text = "Категории";
            this.buttonCategories.UseVisualStyleBackColor = true;
            this.buttonCategories.Click += new System.EventHandler(this.buttonCategories_Click);
            // 
            // buttonArticles
            // 
            this.buttonArticles.Location = new System.Drawing.Point(68, 99);
            this.buttonArticles.Name = "buttonArticles";
            this.buttonArticles.Size = new System.Drawing.Size(179, 23);
            this.buttonArticles.TabIndex = 3;
            this.buttonArticles.Text = "Статьи";
            this.buttonArticles.UseVisualStyleBackColor = true;
            this.buttonArticles.Click += new System.EventHandler(this.buttonArticles_Click);
            // 
            // buttonComments
            // 
            this.buttonComments.Location = new System.Drawing.Point(68, 128);
            this.buttonComments.Name = "buttonComments";
            this.buttonComments.Size = new System.Drawing.Size(179, 23);
            this.buttonComments.TabIndex = 4;
            this.buttonComments.Text = "Комментарии";
            this.buttonComments.UseVisualStyleBackColor = true;
            this.buttonComments.Click += new System.EventHandler(this.buttonComments_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 185);
            this.Controls.Add(this.buttonComments);
            this.Controls.Add(this.buttonArticles);
            this.Controls.Add(this.buttonCategories);
            this.Controls.Add(this.buttonUsers);
            this.Controls.Add(this.buttonRoles);
            this.Name = "FormMain";
            this.Text = "Новостной блог (БД)";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRoles;
        private System.Windows.Forms.Button buttonUsers;
        private System.Windows.Forms.Button buttonCategories;
        private System.Windows.Forms.Button buttonArticles;
        private System.Windows.Forms.Button buttonComments;
    }
}

