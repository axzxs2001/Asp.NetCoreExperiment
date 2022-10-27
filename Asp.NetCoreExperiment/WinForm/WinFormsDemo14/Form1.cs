
using NLog.Windows.Forms;
using System.Windows.Forms;

using NLog;

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
            logger.Fatal("Test");
            logger.Error("Foo");
            logger.Warn("Bar");
            logger.Info("Test");
            logger.Debug("Foo");
            logger.Trace("Bar");

            var form2 = new Form2();
            form2.Show();

        }

        public static Logger logger = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            logger = LogManager.GetCurrentClassLogger(); 
        }
    }
}