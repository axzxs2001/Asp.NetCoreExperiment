namespace WinFormDemo01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           //MessageBox.Show(Environment.CommandLine);
            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                label1.Text = $"主程序：{args[0]},\r\n\r\n参数：{string.Join(',', args[1..])}";
            }  
        }     
    }
}