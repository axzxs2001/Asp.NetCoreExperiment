using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            _eventHub = BlazorService.CretaeBlazorService<Add>(addBlazorWebView);
            _eventHub.OnCallCSharp += EventHub_OnCallCSharp;


        }

        private void EventHub_OnCallCSharp(object sender, object?[]? eventArgs)
        {
            var eventHub = sender as EventHub;
            if (eventHub?.EventName == "clientclick" && eventArgs != null && eventArgs.Length > 0)
            {
                labMessage.Text = eventArgs?[0]?.ToString()!;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _eventHub.CallJS("showtitle", "窗体中数据：" + DateTime.Now);
        }

    }
}
