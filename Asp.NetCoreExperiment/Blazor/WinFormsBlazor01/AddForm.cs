using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsBlazor01.Razors;

namespace WinFormsBlazor01
{
    public partial class AddForm : Form
    {
        IEventHub _eventHub;
        public AddForm()
        {
            InitializeComponent();
            _eventHub = BlazorService.CretaeBlazorService<Add>(addBlazorWebView,controls:button2);
            _eventHub.OnCallCSharpAsync += EventHub_OnCallCSharpAsync;
            txtNo.Text = Guid.NewGuid().ToString("N").ToUpper();
        }

        private async Task<object> EventHub_OnCallCSharpAsync(object sender, object?[]? eventArgs)
        {
            var eventHub = sender as EventHub;
            if (eventHub?.EventName == "clientclick" && eventArgs != null && eventArgs.Length > 0)
            {
                labMessage.Text = "JS事件传过来的参数：" + eventArgs?[0]?.ToString()!;
            }
            return await Task.FromResult(eventArgs?[0]?.ToString()!);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var result = await _eventHub.CallJSAsync("showtitle", txtNo.Text);
            labBackMessage.Text = "WinForm调用JS返回值：" + result.ToString();
        }

        private void AddForm_Load(object sender, EventArgs e)
        {

        }
    }
}
