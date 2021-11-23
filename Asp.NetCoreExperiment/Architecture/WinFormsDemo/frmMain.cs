namespace WinFormsDemo
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox1, "长度不对???111");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(button1, "确定");
            //errorProvider1.Clear();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            var list = new List<Order>() {
new Order{ Name="aaa", Count=1, Description="早上好" },
new Order{ Name="bbb", Count=2, Description="中午好" },
new Order{ Name="ccc", Count=3, Description="好午好" },
            };
            dataGridView1.DataSource = list;
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
          
        }
    }
    class Order
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
    }
}