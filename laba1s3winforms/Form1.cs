using System;
using System.Linq;
using System.Windows.Forms;
using laba1s3core;
namespace laba1s3winforms
{
    public partial class Form1 : Form
    {
        private readonly LibraryLogic _logic;
        public Form1(LibraryLogic logic)
        {
            InitializeComponent();
            _logic = logic;
            RefreshGrid();
        }
        private void RefreshGrid()
        {
            dataGridView1.DataSource = _logic.ReadAllBooks().ToList();
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Номер";
                dataGridView1.Columns[1].HeaderText = "Название";
                dataGridView1.Columns[2].HeaderText = "Автор";
                dataGridView1.Columns[3].HeaderText = "Жанр";
                dataGridView1.Columns[4].HeaderText = "Год";
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var dlg = new BookEditForm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _logic.CreateBook(dlg.Book);
                RefreshGrid();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out var id))
            {
                _logic.DeleteBook(id);
                RefreshGrid();
            }
            else MessageBox.Show("Введите корректный Id");
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out var id))
            {
                var existing = _logic.ReadBook(id);
                if (existing == null) { MessageBox.Show("Книга не найдена"); return; }
                using var dlg = new BookEditForm(existing);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _logic.UpdateBook(dlg.Book);
                    RefreshGrid();
                }
            }
            else MessageBox.Show("Введите корректный Id");
        }
        private void btnSearchAuthor_Click(object sender, EventArgs e) => dataGridView1.DataSource = _logic.FindBooksByAuthor(txtAuthor.Text).ToList();
        private void btnSearchGenre_Click(object sender, EventArgs e) => dataGridView1.DataSource = _logic.FindBooksByGenre(txtGenre.Text).ToList();
        private void btnShowAll_Click(object sender, EventArgs e) => RefreshGrid();
    }
}
