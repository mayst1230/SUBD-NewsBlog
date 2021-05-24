namespace NewsBlogView
{
    partial class FormArticleDocuments
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewArticleDocuments = new System.Windows.Forms.DataGridView();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonChange = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewArticleDocuments)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewArticleDocuments
            // 
            this.dataGridViewArticleDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewArticleDocuments.Location = new System.Drawing.Point(1, 1);
            this.dataGridViewArticleDocuments.Name = "dataGridViewArticleDocuments";
            this.dataGridViewArticleDocuments.Size = new System.Drawing.Size(665, 448);
            this.dataGridViewArticleDocuments.TabIndex = 0;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(672, 12);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(116, 23);
            this.buttonUpdate.TabIndex = 1;
            this.buttonUpdate.Text = "Обновить";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonChange
            // 
            this.buttonChange.Location = new System.Drawing.Point(672, 41);
            this.buttonChange.Name = "buttonChange";
            this.buttonChange.Size = new System.Drawing.Size(116, 23);
            this.buttonChange.TabIndex = 2;
            this.buttonChange.Text = "Обновить КЭШ";
            this.buttonChange.UseVisualStyleBackColor = true;
            this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // FormArticleDocuments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonChange);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.dataGridViewArticleDocuments);
            this.Name = "FormArticleDocuments";
            this.Text = "Статьи (документы)";
            this.Load += new System.EventHandler(this.FormArticleDocuments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewArticleDocuments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewArticleDocuments;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonChange;
    }
}