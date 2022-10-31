
using NLog.Windows.Forms;
using System.Windows.Forms;

using NLog;

namespace WinFormsDemo14
{
    public partial class MainForm : Form
    {
        // LogForm _logForm;
        public MainForm()
        {

            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.LogInfo("开始对账……");
            this.LogError("对账错误");
            this.LogInfo("对账结束");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void 日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var _logForm = new LogForm();
            RichTextBoxTarget.ReInitializeAllTextboxes(_logForm);
            _logForm.Show();
        }
    }
}