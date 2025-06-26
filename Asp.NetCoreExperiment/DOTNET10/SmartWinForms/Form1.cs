using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Reflection;
using System.Xml;

namespace SmartWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var modelID = "gpt-4.1";
            var openAIKey = File.ReadAllText("c://gpt/key.txt");

            var kernel = Kernel.CreateBuilder()
                       .AddOpenAIChatCompletion(modelID, openAIKey).Build();

            var service = kernel.GetRequiredService<IChatCompletionService>();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }




}
