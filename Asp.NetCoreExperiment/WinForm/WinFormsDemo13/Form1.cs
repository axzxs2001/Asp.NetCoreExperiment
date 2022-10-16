using System.Collections.Generic;

namespace WinFormsDemo13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webView21.Source = new Uri(@"http://localhost:5026/home/goods");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var js = """
                $(".btn-info").click()
                """;
            webView21.CoreWebView2.ExecuteScriptAsync(js);
        }
        int sn = 1;
        private void button2_Click(object sender, EventArgs e)
        {
            int mark = DateTime.Now.Millisecond * 1000 + DateTime.Now.Microsecond;
            var js = $"""   
                var goods=document.querySelector('[x-data]')._x_dataStack[0].Goods;
                goods.GoodsTypeID={mark % 2 + 1};
                goods.Name="商品{mark}";
                goods.Price={100 * new Random().Next(1, 20)};
                goods.Describe="商品{mark}说明";
                goods.SerialNumber={sn};
                goods.Validate={(mark % 2 == 0).ToString().ToLower()};
                goods.GoodsType="{(mark % 2 == 0 ? "A类型" : "B类型")}";
                """;
            webView21.CoreWebView2.ExecuteScriptAsync(js);
            sn++;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var js = """
                $("#addgoods .btn-primary").click()
                """;
            webView21.CoreWebView2.ExecuteScriptAsync(js);
        }
      

        private void CoreWebView2_ScriptDialogOpening(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2ScriptDialogOpeningEventArgs e)
        {
            e.Accept();
        }

        private void webView21_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                webView21.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = false;
                webView21.CoreWebView2.ScriptDialogOpening += CoreWebView2_ScriptDialogOpening;
            }
        }
    }
}