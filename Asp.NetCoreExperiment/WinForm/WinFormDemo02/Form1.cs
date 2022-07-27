using System.Drawing;

namespace WinFormDemo02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
          
        }
     

        private void switch1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = switch1.Checked ? "选中" : "没选中";
        }
    }
}