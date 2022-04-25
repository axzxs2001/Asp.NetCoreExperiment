using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
namespace WinFormsBlazor03
{
    public partial class chartForm : Form
    {
        public chartForm()
        {
            InitializeComponent();
            timer1.Enabled = false;
            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            services.AddSingleton(this.button1);
            services.AddSingleton(this.timer1);
            blazorWebView1.HostPage = "wwwroot\\index.html";
            blazorWebView1.Services = services.BuildServiceProvider();
            blazorWebView1.RootComponents.Add<DomePage>("#app");

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}