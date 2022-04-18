using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using WinFormsBlazor01.Services;
using WinFormsBlazor01.Razors;
using Microsoft.JSInterop;


namespace WinFormsBlazor01
{
    public partial class MainForm : Form
    {
        IEventHub _eventHub;
        public MainForm()
        {
            InitializeComponent();
            _eventHub = BlazorService.CretaeBlazorService<IExamService, ExamService, Query>(queryBlazorWebView);
            _eventHub.OnCallDotNet += _eventHub_OnCallDotNet;


        }

        private void _eventHub_OnCallDotNet(object sender, string eventName, object?[]? eventArgs)
        {
            if (eventName == "clientclick" && eventArgs != null && eventArgs.Length > 0)
            {
                MessageBox.Show(eventArgs[0]?.ToString());
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            _eventHub.CallJS("showtitle", "窗体中数据：" + DateTime.Now);
        }
    }

}