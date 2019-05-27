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

        public string language = Properties.Settings.Default.Laugue;
        public frmMain()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Localize("ja-JP"); 

        }

 
    }
}


