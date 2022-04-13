using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;

namespace WinFormsBlazor01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            var services = new ServiceCollection();
            services.AddBlazorWebView();
            blazorWebView1.HostPage = "wwwroot\\index.html";
            blazorWebView1.Services = services.BuildServiceProvider();
            blazorWebView1.RootComponents.Add<Counter>("#app");
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}