using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverSurgery
{
    public partial class LogIn : Form
    {
        //This creates an array that enables the program to display a welcome message once it starts.
        public string[] Pages = new string[1];
        public LogIn()
        {
            InitializeComponent();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            Pages[0] = richTextBox1.Text = ("Welcome ... to the Over Surgery");
            if (btnLogin.Visible == true)
            {
                richTextBox1.Visible = true;
            }

        }
        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            btnOK.Show();
            if (btnOK.Visible == true)
            {
                btnExit.Visible = true;
                txtPassword.Visible = true;
                txtUser.Visible = true;
                labPassword.Visible = true;
                labUsername.Visible = true;
                richTextBox1.Visible = false;
                btnLogin.Visible = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // This confirms a correct username and password
            if (txtUser.Text == "user" && txtPassword.Text == "user")
            {
                this.Hide();
                MainBackGround form1 = new MainBackGround(this);
                form1.Show();
            }

            //This displayes a message for a wrong username and password
            else
            {
                MessageBox.Show("Error: Invalid Username or Password");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //This Closes the program.
            this.Close();
        }
    }

}