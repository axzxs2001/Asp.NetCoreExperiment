using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsDemo12
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        void Draw()
        {

            using var graphics = this.CreateGraphics();
            var brush = new HatchBrush(HatchStyle.Cross, Color.Black, Color.White);
            graphics.FillEllipse(brush, 0, 0, 300, 300);

            graphics.FillPie(brush, 0, 300, 300, 300,-70,100);
        }
        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }
    }
}
