namespace WinFormsDemo14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.SetStyle(ControlStyles.UserMouse, true);

           MessageBox.Show( SystemInformation.MouseButtons.ToString());
        }
    }
}