using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormDemo05
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            checkBox2.Text = "GrowOnly";
        }
        int i = 10;
        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Add(new Button() { Text = DateTime.Now.ToString(), Width = 100, Top = i + 30, Height = 30 });
            this.Text = panel1.Height.ToString();
            i += 35;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count > 0)
            {
                panel1.Controls.RemoveAt(panel1.Controls.Count - 1);
                this.Text = panel1.Height.ToString();
                i -= 35;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                panel1.AutoSize = true;
            }
            else
            {
                panel1.AutoSize = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox2.Text = "GrowAndShrink";
                panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            }
            else
            {
                checkBox2.Text = "GrowOnly";
                panel1.AutoSizeMode = AutoSizeMode.GrowOnly;
            }
        }
    }
}
