using NLog;
using NLog.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NLog.Windows.Forms.RichTextBoxTarget;

namespace WindowsFormsNLog
{
    public partial class frmLog : Form
    {
        protected static NLog.Logger _log = null;
        public frmLog()
        {
            InitializeComponent();
            _log = _log ?? LogManager.GetCurrentClassLogger();
            RichTextBoxTarget.ReInitializeAllTextboxes(this);
            RichTextBoxTarget.GetTargetByControl(rtbLog).LinkClicked += DelLink_Clicked;

        }
        void DelLink_Clicked(RichTextBoxTarget sender, string linkText, LogEventInfo logEvent)
        {
            if (logEvent.Exception != null)
            {
                MessageBox.Show(logEvent.Exception.ToString(), "Exception details", MessageBoxButtons.OK);
            }
        }

        private void FrmLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
