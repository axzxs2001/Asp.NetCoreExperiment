namespace WinFormDemo07
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        //�����Show��ShowDialog

        private void �½�NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var child01 = new Child01Form();
            child01.MdiParent = this;
            child01.WindowState = FormWindowState.Maximized;
            child01.Show();
        }

        private void �½�NToolStripButton_Click(object sender, EventArgs e)
        {
            var child02 = new Child02Form();
            child02.MdiParent = this;
            child02.WindowState = FormWindowState.Maximized;
            child02.Show();
        }
    }
}