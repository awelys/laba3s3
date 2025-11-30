using System;
using System.Linq;
using System.Windows.Forms;
using laba1s3core;

namespace laba1s3winforms
{
    public partial class Form1 : Form
    {
        private readonly ILibraryLogic _logic;

        public Form1(ILibraryLogic logic)
        {
            _logic = logic ?? throw new ArgumentNullException(nameof(logic));
            InitializeComponent();
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении списка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using var dlg = new BookEditForm();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _logic.CreateBook(dlg.Book);
                    RefreshGrid();
                    MessageBox.Show("Книга успешно добавлена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка валидации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении книги: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(txtId.Text, out var id))
                {
                    var result = MessageBox.Show($"Вы уверены, что хотите удалить книгу с ID {id}?", 
                        "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        var deleted = _logic.DeleteBook(id);
                        if (deleted)
                        {
                            RefreshGrid();
                            MessageBox.Show("Книга успешно удалена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Книга не найдена", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректный Id", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(txtId.Text, out var id))
                {
                    var existing = _logic.ReadBook(id);
                    if (existing == null)
                    {
                        MessageBox.Show("Книга не найдена", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    using var dlg = new BookEditForm(existing);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        var updated = _logic.UpdateBook(dlg.Book);
                        if (updated)
                        {
                            RefreshGrid();
                            MessageBox.Show("Книга успешно обновлена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Не удалось обновить книгу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректный Id", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка валидации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchAuthor_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = _logic.FindBooksByAuthor(txtAuthor.Text).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchGenre_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = _logic.FindBooksByGenre(txtGenre.Text).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }
    }
}
