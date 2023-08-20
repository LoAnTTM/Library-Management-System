using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Library_Management_System
{
    public partial class book_manager : Form
    {
        private string filePath = @"D:\Asm2\Library Management System\Library Management System\data.csv";
        public book_manager()
        {
            InitializeComponent();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            //Clear any default columns
            dataGridView1.Columns.Clear();
            //Add columns to the dataGridView
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Author", "Author");
            dataGridView1.Columns.Add("Category", "Categoty");
            dataGridView1.Columns.Add("Year", "year");
            //Other Settings
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
        }
        private void AddBook(Book book)
        {
            //write to csv
            using (StreamWriter writer = new StreamWriter(filePath, true)) 
            {
                writer.WriteLine($"{book.bookId},{book.bookName},{ book.authorName},{ book.category},{ book.publicYear}");
            }
            //Add to DataGrid
            dataGridView1.Rows.Add(book.bookId, book.bookName, book.authorName, book.category, book.publicYear);
        }
        private void LoadFile()
        {
            //Assuming the DataGribView Data source is a list book
            var books = dataGridView1.DataSource as List<Book>;

            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                foreach (var book in books)
                {
                    writer.WriteLine($"{book.bookId},{book.bookName},{book.authorName},{book.category},{book.publicYear}");
                }
            }
        }
        private void btn_new_Click(object sender, EventArgs e)
        {
            txtid.Text = "";
            txtname.Text = "";
            txtauthor.Text = "";
            txtcategory.Text = "";
            txtyear.Text = "";
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Book newBook = new Book()
            {
                bookId = int.Parse(txtid.Text),
                bookName = txtname.Text,
                authorName = txtauthor.Text,
                category = txtcategory.Text,
                publicYear = int.Parse(txtyear.Text),
            };
            AddBook(newBook);
            MessageBox.Show("Book added successfully!");
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to edit.");
            }

            DataGridViewRow selectedRow = dataGridView1.Rows[0];
            Book selectedBook = new Book()
            {
                bookId = int.Parse(selectedRow.Cells["ID"].Value.ToString()),
                bookName = selectedRow.Cells["Name"].Value.ToString(),
                authorName = selectedRow.Cells["Author"].Value.ToString(),
                category = selectedRow.Cells["Category"].Value.ToString(),
                publicYear = int.Parse(selectedRow.Cells["Year"].Value.ToString()),
            };

            //Get values form text boxes
            selectedBook.bookId = int.Parse(txtid.Text);
            selectedBook.bookName = txtname.Text;
            selectedBook.authorName = txtauthor.Text;
            selectedBook.category = txtcategory.Text;
            selectedBook.publicYear = int.Parse(txtyear.Text);

            //Update book to GridView
            selectedRow.Cells["ID"].Value = selectedBook.bookId;
            selectedRow.Cells["Name"].Value = selectedBook.bookName;
            selectedRow.Cells["Author"].Value = selectedBook.authorName;
            selectedRow.Cells["Category"].Value = selectedBook.category;
            selectedRow.Cells["Year"].Value = selectedBook.publicYear;

            //Update file
            LoadFile();
            MessageBox.Show("Book details update successfully!");
        }

        private void btn_load_Click(object sender, EventArgs e)
        {

        }
    }
}
