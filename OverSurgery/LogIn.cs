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
        public LogIn()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "user" && txtPassword.Text == "user")
            {
                this.Hide();
                MainBackGround form1 = new MainBackGround(this);
                form1.Show();
            }
            else
            {
                MessageBox.Show("Error: Invalid Username or Password");
            }
        }

       
    }
}
