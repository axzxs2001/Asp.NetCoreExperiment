

using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.IO;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormPDFRevie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
      

            var txt = Directory.GetCurrentDirectory() + "\\a.txt";
            var arr = Convert.FromBase64String(File.ReadAllText(txt));
            var filename = Directory.GetCurrentDirectory() + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
            FileStream str = new FileStream(filename, FileMode.Create);
            str.Write(arr, 0, arr.Length);

            WebView2 webView = new WebView2();
            webView.Dock = DockStyle.Fill;
            panel1.Controls.Add(webView);
            webView.Source = new Uri(filename);

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}