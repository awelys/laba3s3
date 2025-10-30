namespace laba1s3winforms
{
    partial class BookEditForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.txtGenre = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.txtTitle.Location = new System.Drawing.Point(30, 20); this.txtTitle.Name = "txtTitle"; this.txtTitle.PlaceholderText = "Название"; this.txtTitle.Size = new System.Drawing.Size(300, 27);
            this.txtAuthor.Location = new System.Drawing.Point(30, 60); this.txtAuthor.Name = "txtAuthor"; this.txtAuthor.PlaceholderText = "Автор"; this.txtAuthor.Size = new System.Drawing.Size(300, 27);
            this.txtGenre.Location = new System.Drawing.Point(30, 100); this.txtGenre.Name = "txtGenre"; this.txtGenre.PlaceholderText = "Жанр"; this.txtGenre.Size = new System.Drawing.Size(300, 27);
            this.txtYear.Location = new System.Drawing.Point(30, 140); this.txtYear.Name = "txtYear"; this.txtYear.PlaceholderText = "Год"; this.txtYear.Size = new System.Drawing.Size(100, 27);
            this.btnOk.Location = new System.Drawing.Point(30, 190); this.btnOk.Name = "btnOk"; this.btnOk.Size = new System.Drawing.Size(100, 29); this.btnOk.Text = "ОК"; this.btnOk.UseVisualStyleBackColor = true; this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            this.btnCancel.Location = new System.Drawing.Point(150, 190); this.btnCancel.Name = "btnCancel"; this.btnCancel.Size = new System.Drawing.Size(100, 29); this.btnCancel.Text = "Отмена"; this.btnCancel.UseVisualStyleBackColor = true; this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.ClientSize = new System.Drawing.Size(380, 250);
            this.Controls.Add(this.txtTitle); this.Controls.Add(this.txtAuthor); this.Controls.Add(this.txtGenre); this.Controls.Add(this.txtYear); this.Controls.Add(this.btnOk); this.Controls.Add(this.btnCancel);
            this.Name = "BookEditForm"; this.Text = "Книга"; this.ResumeLayout(false); this.PerformLayout();
        }
        private System.Windows.Forms.TextBox txtTitle; private System.Windows.Forms.TextBox txtAuthor; private System.Windows.Forms.TextBox txtGenre; private System.Windows.Forms.TextBox txtYear; private System.Windows.Forms.Button btnOk; private System.Windows.Forms.Button btnCancel;
    }
}
