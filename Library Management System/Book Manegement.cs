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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net;

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
            dataGridView1.Columns.Add("Year", "Year");
            dataGridView1.Columns.Add("Status", "Status");
            //Other Settings
            //dataGridView1.AllowUserToAddRows = false;
            //dataGridView1.AllowUserToDeleteRows = false;
            //dataGridView1.ReadOnly = true;
        }
        private void AddBook(Book book)
        {
            //write to csv
            using (StreamWriter writer = new StreamWriter(filePath, true)) 
            {
                writer.WriteLine($"{book.bookId},{book.bookName},{ book.authorName},{ book.category},{ book.publicYear},");
            }
            //Add to DataGrid
            dataGridView1.Rows.Add(book.bookId, book.bookName, book.authorName, book.category, book.publicYear);
        }
        private void UpdateFile()
        {
            //Assuming the DataGribView Data source is a list book
            var books = dataGridView1.DataSource as List<Book>;

            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                foreach (var book in books)
                {
                    writer.WriteLine($"{book.bookId},{book.bookName},{book.authorName},{book.category},{book.publicYear},{book.status}"+ ",");
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
                status = txtstatus.Text,
            };
            AddBook(newBook);
            MessageBox.Show("Book added successfully!");
        }
        

        private void btn_load_Click(object sender, EventArgs e)
        {
            //check if the file exists
            if (File.Exists(filePath))
            {
                //Open file to read 
                using (StreamReader reader = new StreamReader(filePath)) 
                {
                    //Loop until read all line in file
                    while(!reader.EndOfStream) 
                    {
                        string line = reader.ReadLine();
                        //Make sure the line not empty or null
                        if(!string.IsNullOrEmpty(line)) 
                        {
                            string[] value = line.Split(',');
                            //Check if the row has 6 columns or not
                            if(value.Length == 6)
                            {
                                string status = string.IsNullOrEmpty(value[5]) ? "Free" : "Borrower";
                                dataGridView1.Rows.Add(value[0], value[1], value[2], value[3], value[4], status);
                            }
                            else
                            {
                                MessageBox.Show("Skipped line");
                            }
                        }
                    }
                }
                MessageBox.Show("Book loaded successfully!");
            }
            else
            {
                MessageBox.Show("File does not exist.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            search_book s = new search_book();
            s.Show();
            this.Hide();
        }

        private void btn_borrow_Click(object sender, EventArgs e)
        {
            borrow_book br = new borrow_book();
            br.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtauthor.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtcategory.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtyear.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }
    }
}
