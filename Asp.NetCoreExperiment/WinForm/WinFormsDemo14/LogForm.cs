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

namespace WinFormsDemo14
{
    public partial class LogForm : Form
    {
        public LogForm()
        {            
            InitializeComponent();
        }


        private void LogForm_Load(object sender, EventArgs e)
        {

        }
    }
    public static class LoggerExpand
    {
        public static void LogInfo(this Form form, string message)
        {
            _logger.Info(message);
        }
        public static void LogTrace(this Form form, string message)
        {
            _logger.Trace(message);
        }
        public static void LogError(this Form form, string message)
        {
            _logger.Error(message);
        }
        public static void LogDebug(this Form form, string message)
        {
            _logger.Debug(message);
        }
        public static void LogFatal(this Form form, string message)
        {
            _logger.Fatal(message);
        }
        public static void LogWarn(this Form form, string message)
        {
            _logger.Warn(message);
        }
        public static void LogInfo(this Form form, LogLevel level, string message)
        {
            _logger.Log(level, message);
        }
        static Logger _logger => LogManager.GetCurrentClassLogger();
    }
}
