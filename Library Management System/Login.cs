using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string user_name = tb_userName.Text;
            string password = tb_password.Text;
            if (user_name == "admin" && password == "123")
            {
                book_manager b = new book_manager();
                b.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username or password is incorrect.");
                tb_userName.Clear();
                tb_password.Clear();
                tb_userName.Focus();
            }
        }

        private void btn_quit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
