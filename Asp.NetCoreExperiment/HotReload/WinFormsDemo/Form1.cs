namespace WinFormsDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Text = DateTime.Now.ToString();
            MessageBox.Show(DateTime.Now.ToString());
        }
    }
}