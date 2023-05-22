using System.ComponentModel;

namespace WinForm_GPTDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var person = new BaseInfo()
            {
                psn_no = "psn_no",
                psn_cert_type = "psn_cert_type",
                certno = "certno",
                psn_name = "psn_name",
                gend = "gend",
                naty = "naty",
                brdy = DateTime.Now,
                age = 1,
                expContent = "expContent"
            };
            psn_no_Lab.Text = person.psn_no;
            //ΩË÷˙Githu Copilot£¨ µœ÷∏≥÷µ
            psn_cert_type_Lab.Text = person.psn_cert_type;
            certno_Lab.Text = person.certno;
            psn_name_Lab.Text = person.psn_name;
            gend_Lab.Text = person.gend;
            naty_Lab.Text = person.naty;
            brdy_Lab.Text = person.brdy.ToString();
            age_Lab.Text = person.age.ToString();
            expContent_Lab.Text = person.expContent;

        }
    }

}