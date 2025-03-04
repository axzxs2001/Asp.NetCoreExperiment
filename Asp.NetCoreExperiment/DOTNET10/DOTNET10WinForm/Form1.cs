namespace DOTNET10WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataAsJson("yyyy-MM-dd HH:mm:ss", DateTime.Now);
        }
    }
}
