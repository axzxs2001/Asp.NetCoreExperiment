using System.ComponentModel;

namespace WinFormsDemo06
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();         
        }
        bool mark = true;
        private void startButton_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                var times = 0;
                while (mark)
                {
                    this.Invoke(() =>
                    {
                        messageLabel.Text = $"正在上传第{times++}条！";
                    });
                    SpinWait.SpinUntil(() => false, 100);
                }
                this.Invoke(() =>
                {
                    this.Close();
                });
            });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mark)
            {
                e.Cancel = true;
                mark = false;
           
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                var times = 0;
                while (true)
                {
                    this.Invoke(() =>
                    {
                        messageLabel.Text = $"正在上传第{times++}条！";
                    });
                    SpinWait.SpinUntil(() => false, 100);
                }              
            });
        }
    }
}