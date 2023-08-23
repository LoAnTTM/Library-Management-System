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
    public partial class borrow_book : Form
    {
        private string filePath = @"D:\Asm2\Library Management System\Library Management System\data.csv";
        public borrow_book()
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
            //Assuming the DataGribView Data source is a list book
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
                            //Check if the row has 5 columns or not
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
        private void UpdateFile()
        {
            //Use StringBuilder để thao tác với chuỗi hiệu quả hơn để nối thêm hoặc sửa chuỗi
            StringBuilder sb = new StringBuilder();
            //Loop though each row in table
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string[] rowValues = new string[row.Cells.Count];
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    //lấy các giá trị cho từng cột trong hàng
                    rowValues[i] = row.Cells[i].Value.ToString();
                }
                //nối các giá trị dưới dạng dòng với dấu ','
                sb.AppendLine(string.Join(",", rowValues));
            }

            File.WriteAllText(filePath, sb.ToString());
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string search_text = txt_bname.Text.ToLower();
            //Clear the table
            dataGridView1.Rows.Clear();
            //Read and filter the input book from the text box
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                        {
                            string[] value = line.Split(',');
                            //Check if columns contain value in text box
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

        private void btn_borrow_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;

                string borrower = txt_mname.Text;

                if (string.IsNullOrEmpty(borrower))
                {
                    MessageBox.Show("Please enter borrower's name.");
                    return;
                }

                // Assuming the 6th column in dataGridView contains the borrower's name
                if (dataGridView1.Rows[selectedRowIndex].Cells[5].Value.ToString() == "")
                {
                    dataGridView1.Rows[selectedRowIndex].Cells[5].Value = borrower;

                    // Update the CSV file as well
                    UpdateFile();
                }
                else
                {
                    MessageBox.Show("This book is already borrowed.");
                }
            }
        }
        private void btn_return_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;

                // Clear borrower's name in DataGridView
                dataGridView1.Rows[selectedRowIndex].Cells[5].Value = "";

                // Update the CSV file as well
                UpdateFile();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //txt_bname.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }
    }
}
