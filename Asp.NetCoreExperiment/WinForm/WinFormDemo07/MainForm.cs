namespace WinFormDemo07
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        //窗体的Show和ShowDialog

        private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var child01 = new Child01Form();
            child01.MdiParent = this;
            child01.WindowState = FormWindowState.Maximized;
            child01.Show();
        }

        private void 新建NToolStripButton_Click(object sender, EventArgs e)
        {
            var child02 = new Child02Form();
            child02.MdiParent = this;
            child02.WindowState = FormWindowState.Maximized;
            child02.Show();
        }
    }
}