using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using WinFormsBlazor01.Services;
using WinFormsBlazor01.Razors;
using Microsoft.JSInterop;


namespace WinFormsBlazor01
{
    public partial class QueryForm : Form
    {

        public QueryForm()
        {
            InitializeComponent();
            BlazorService.CretaeBlazorService<IExamService, ExamService, Query>(queryBlazorWebView); 
        }
    


        private void button1_Click(object sender, EventArgs e)
        {
            var addForm = new AddForm();
            addForm.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }

}