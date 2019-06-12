using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFGlobalizationLocalization;
namespace WFGlobalizationLocalization
{
    public partial class frmMain : Form
    {

        public ABC abc;

        ComponentResourceManager _resource;
        public string language = Properties.Settings.Default.Laugue;
        public frmMain()
        {
            abc = new ABC();
            InitializeComponent();
            _resource = new ComponentResourceManager(typeof(frmMain));
            //加载系统默认语言
            this.Localize(language);
            comboBox1.SelectedItem = language;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_resource.GetString("messagetext"));

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Localize(comboBox1.SelectedItem.ToString());
            Properties.Settings.Default.Laugue = comboBox1.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void YyyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

    public class ABC
    {
        public string A { get; set; }

        public string B { get; set; }
    }
}


