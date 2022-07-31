namespace WinFormDemo03
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = $"这是一个系统【工号：{Common.CurrentUser?.ID}，名称：{Common.CurrentUser?.Name}】";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}