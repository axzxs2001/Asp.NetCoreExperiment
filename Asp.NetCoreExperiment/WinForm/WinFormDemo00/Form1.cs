using System.Diagnostics;

namespace WinFormDemo00
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            var pro = Process.Start(@"C:\MyFile\Asp.NetCoreExperiment\Asp.NetCoreExperiment\WinForm\WinFormDemo01\bin\Debug\net7.0-windows\WinFormDemo01.exe", new string[] { "gsw", "abcd" }); 
            pro.EnableRaisingEvents = true;
            pro.Exited += Pro_Exited;
        }

        private void Pro_Exited(object? sender, EventArgs e)
        {
            this.Invoke((object sender) =>
            {
                label1.Text = $"{DateTime.Now.ToShortTimeString()}，WinFormDemo01返回值：{(sender as Process)?.ExitCode.ToString()}";

            }, sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var pro = Process.Start(@"C:\MyFile\Asp.NetCoreExperiment\Asp.NetCoreExperiment\WinForm\WinFormDemo01\bin\Debug\net7.0-windows\WinFormDemo01.exe", new string[] { "gsw", "abcd" });
            pro.WaitForExit();//阻塞       
            label1.Text = $"{DateTime.Now.ToShortTimeString()}，WinFormDemo01返回值：{pro.ExitCode.ToString()}";
        }
    }
}