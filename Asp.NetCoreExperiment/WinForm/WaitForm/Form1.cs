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
                LoadingManage.Message = $"����ִ�е�{i}�����ݡ���������������";
                Thread.Sleep(1000);
            }
            LoadingManage.Close();



        }
    }

    
}