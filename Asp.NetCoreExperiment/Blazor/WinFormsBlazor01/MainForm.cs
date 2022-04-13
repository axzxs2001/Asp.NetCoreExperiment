using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using WinFormsBlazor01.Services;
using WinFormsBlazor01.Razors;

namespace WinFormsBlazor01
{
    public partial class MainForm : Form
    {
        ServiceCollection services = new ServiceCollection();
        public MainForm()
        {
            InitializeComponent();

            services.AddWindowsFormsBlazorWebView();
            services.AddSingleton<IExamService, ExamService>();
            queryBlazorWebView.HostPage = "wwwroot\\index.html";
            queryBlazorWebView.Services = services.BuildServiceProvider();
            queryBlazorWebView.RootComponents.Add<Query>("#app");
            services.AddSingleton(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
          

        }
    }
}