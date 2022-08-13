using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WinFormDemo07
{
    public partial class MainForm : Form
    {
        private readonly IDataService _dataService;
        private readonly ILogger<MainForm> _logger;
        private readonly Child01Form _childo1form;
        public MainForm(Child01Form childo1form, ILogger<MainForm> logger,IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
            _logger.LogInformation("MainForm 启动");
            _logger.LogError("MainForm 启动");
            _childo1form = childo1form;
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        //窗体的Show和ShowDialog

        private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logger.LogInformation("child01form 启动");
            var child01 = Program.ServiceProvider.GetRequiredService<Child01Form>();

            //var child01 = new Child01Form();
            child01.MdiParent = this;
            child01.WindowState = FormWindowState.Maximized;
            child01.Show();

            //child01.Close();
            //child01.Hide();
            //child01.Dispose();

        }

        private void 新建NToolStripButton_Click(object sender, EventArgs e)
        {

            var child02 = new Child02Form();
            child02.MdiParent = this;
            child02.WindowState = FormWindowState.Maximized;
            child02.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form1 = new Form1();
            form1.ShowInTaskbar = false;
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form1 = new Form1();
            form1.ShowInTaskbar = false;
            form1.ShowDialog();

        }
        Form1 form1 = new Form1();
        private void button3_Click(object sender, EventArgs e)
        {
            form1.ShowInTaskbar = false;
            form1.Show();
            //form1.ShowDialog();
            //this.Text = Application.OpenForms.Count.ToString();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.Text = Application.OpenForms.Count.ToString();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //  e.Cancel = true;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MessageBox.Show(e.CloseReason.ToString());

        }

    }
}