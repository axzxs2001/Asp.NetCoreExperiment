using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsNLog
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        frmLog log = null;
        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            log = log ?? new frmLog();         
            log.MdiParent = this;
            log.Show();
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            var demo = new frmDemo();
            demo.MdiParent = this;
            demo.Show();
        }
    }
}
