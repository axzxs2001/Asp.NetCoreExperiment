using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeberShipDemo2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {       
            var ph = new Microsoft.AspNet.Identity.PasswordHasher();
            hashPassword_tb.Text = ph.HashPassword(password_tb.Text);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var ph = new Microsoft.AspNet.Identity.PasswordHasher();
            MessageBox.Show(ph.VerifyHashedPassword(hashPassword_tb.Text, password_tb.Text).ToString());
        }
    }  
}
