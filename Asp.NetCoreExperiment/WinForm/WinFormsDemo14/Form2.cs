using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsDemo14
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.logger.Fatal("Test");
            Form1.logger.Error("Foo");
            Form1.logger.Warn("Bar");
            Form1.logger.Info("Test");
            Form1.logger.Debug("Foo");
            Form1.logger.Trace("Bar");
        }
    }
}
