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

namespace WindowsFormsNLog
{
    public partial class frmDemo : Form
    {
        readonly Logger _log;
        public frmDemo()
        {
            InitializeComponent();

            _log = _log ?? LogManager.GetCurrentClassLogger();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _log.Info("Info");
            _log.Trace("Trace");
            _log.Warn("Warn");
            _log.Error(new Exception("这是一个错误！！"),"Error");
            _log.Debug("Debug");
            _log.Fatal(new Exception("这是一个异常"), "Fatal");
        }
    }
}
