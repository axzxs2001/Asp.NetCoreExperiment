namespace WinFormNet9Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var scm = (SystemColorMode)Enum.Parse(typeof(SystemColorMode), comboBox1.SelectedItem.ToString());
            Application.SetColorMode(scm);
            //this.FormBorderColor = SystemColors.ActiveBorder;
            //this.FormCaptionBackColor = SystemColors.ActiveCaption;
            this.Refresh();


            //comboBox1.BackColor = SystemColors.Control;

            comboBox1.Refresh();

            textBox1.Refresh();


            //MessageBox.Show("hi");
            //var form = new Form1();
            //form.ShowDialog();

            //var bmp = new Bitmap(Image.FromFile("Microsoft_logo.png"));
            //var invertEffect = new System.Drawing.Imaging.Effects.InvertEffect();
            //bmp.ApplyEffect(invertEffect);
            //bmp.Save(DateTime.Now.ToString("ddHHmmssfff") + ".png");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var item in Enum.GetValues(typeof(SystemColorMode)))
            {
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void Form1_SystemColorsChanged(object sender, EventArgs e)
        {


        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("toolStripButton3_Click");
        }

        private void toolStripButton3_SelectedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new Form1();
            form.Show();
        }
    }
}
