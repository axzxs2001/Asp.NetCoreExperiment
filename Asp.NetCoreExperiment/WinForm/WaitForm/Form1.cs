using System.Runtime.CompilerServices;

namespace WaitForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadingManage.Show();
            for (var i = 0; i < 3; i++)
            {
                LoadingManage.Message = $"正在执行第{i}条数据……………………";
                Thread.Sleep(1000);
            }
            LoadingManage.Close();



        }
    }

    
}