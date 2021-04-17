using BlazorWinForm.wwwroot;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Dapper;


namespace BlazorWinForm
{
    public partial class frmMain : Form
    {
        public frmMain()
        {

            InitializeComponent();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddBlazorWebView();
            var blazor = new BlazorWebView()
            {
                Width = this.Width - 5,
                Height = this.Height - 5,
                // Dock = DockStyle.Fill,
                HostPage = "wwwroot/index.html",
                Services = serviceCollection.BuildServiceProvider(),

            };
            blazor.RootComponents.Add<Query>("#app");
            Controls.Add(blazor);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }




    }

    class Goods
    {
        public string spid { get; set; }
        public string spmch { get; set; }
        public string shpchd { get; set; }
        public string shpgg { get; set; }
    }
}
