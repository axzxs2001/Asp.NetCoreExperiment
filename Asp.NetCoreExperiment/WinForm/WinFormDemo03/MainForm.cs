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
            this.Text = $"����һ��ϵͳ�����ţ�{Common.CurrentUser?.ID}�����ƣ�{Common.CurrentUser?.Name}��";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}