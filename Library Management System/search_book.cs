using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class search_book : Form
    {
        private string filePath = @"D:\Asm2\Library Management System\Library Management System\data.csv";
        public search_book()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadFile();
        }
        private void SetupDataGridView()
        {
            //Clear any default columns
            while (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(0);
            }
            //Add columns to the dataGridView
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Author", "Author");
            dataGridView1.Columns.Add("Category", "Categoty");
            dataGridView1.Columns.Add("Year", "Year");
            dataGridView1.Columns.Add("Status", "Status");
            //Other Settings
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
        }
        private void LoadFile()
        {
            //check if the file exists
            if (File.Exists(filePath))
            {
                //Open file to read 
                using (StreamReader reader = new StreamReader(filePath))
                {
                    //Loop until read all line in file
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        //Make sure the line not empty or null
                        if (!string.IsNullOrEmpty(line))
                        {
                            string[] value = line.Split(',');
                            //Check if the row has 6 columns or not
                            if (value.Length == 6)
                            {
                                string status = string.IsNullOrEmpty(value[5]) ? "Free" : "Borrower";
                                dataGridView1.Rows.Add(value[0], value[1], value[2], value[3], value[4], status);
                            }
                        }
                    }
                }
            }
        }
        private void btn_search_Click(object sender, EventArgs e)
        {
            string search_text = txtsearch.Text.ToLower();
            //Clear the table
            while (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(0);
            }
            //Read and filter the input book from the text box
            if (File.Exists(filePath)) 
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while(!reader.EndOfStream) 
                    {
                        string line = reader.ReadLine();
                        if(!string.IsNullOrEmpty(line)) 
                        {
                            string[] value = line.Split(',');
                            //Check if columns contain value in text box
                            if (value.Any(column => column.ToLower().Contains(search_text)))
                            {
                                dataGridView1.Rows.Add(value);
                            }

                        }
                    }
                }
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            book_manager b = new book_manager();
            b.Show();
            this.Hide();
        }
    }
}
