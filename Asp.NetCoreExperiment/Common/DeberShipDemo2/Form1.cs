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
            textBox1.Text = ph.HashPassword("111111!");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var ph = new Microsoft.AspNet.Identity.PasswordHasher();
            MessageBox.Show(ph.VerifyHashedPassword(textBox1.Text, "Nss#123456").ToString());
        }
    }  
}
