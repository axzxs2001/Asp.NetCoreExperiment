
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
            this.LogInfo("��ʼ���ˡ���");
            this.LogError("���˴���");
            this.LogInfo("���˽���");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ��־ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var _logForm = new LogForm();
            RichTextBoxTarget.ReInitializeAllTextboxes(_logForm);
            _logForm.Show();
        }
    }
}