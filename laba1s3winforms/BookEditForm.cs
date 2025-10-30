using System;
using System.Windows.Forms;
using laba1s3core;
namespace laba1s3winforms
{
    public partial class BookEditForm : Form
    {
        public Book Book { get; private set; }
        public BookEditForm(Book? book = null)
        {
            InitializeComponent();
            if (book != null) { Book = new Book { Id = book.Id, Title = book.Title, Author = book.Author, Genre = book.Genre, Year = book.Year }; txtTitle.Text = Book.Title; txtAuthor.Text = Book.Author; txtGenre.Text = Book.Genre; txtYear.Text = Book.Year.ToString(); }
            else Book = new Book();
        }
        private void btnOk_Click(object sender, EventArgs e) { Book.Title = txtTitle.Text; Book.Author = txtAuthor.Text; Book.Genre = txtGenre.Text; Book.Year = int.TryParse(txtYear.Text, out var y) ? y : 0; DialogResult = DialogResult.OK; Close(); }
        private void btnCancel_Click(object sender, EventArgs e) { DialogResult = DialogResult.Cancel; Close(); }
    }
}
