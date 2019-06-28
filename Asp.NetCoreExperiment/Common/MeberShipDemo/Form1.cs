using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeberShipDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var ph = new Microsoft.AspNet.Identity.PasswordHasher<ApplicationUser>();
            textBox1.Text = ph.HashPassword(new ApplicationUser()
            {               
                SecurityStamp = "45b85708-5851-45cf-998b-3563d4047d22"
            }, "111111!"); 
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var ph = new Microsoft.AspNet.Identity.PasswordHasher<ApplicationUser>();
            MessageBox.Show(ph.VerifyHashedPassword(new ApplicationUser()
            {                
                SecurityStamp = "45b85708-5851-45cf-998b-3563d4047d22"
            }, textBox1.Text, "Nw668099!").ToString());
        }
    }

    public class ApplicationUser : Microsoft.AspNet.Identity.EntityFramework.IdentityUser
    {
        public string FullName { get; set; }  

    
    }
}
