using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;
using OllamaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
#pragma warning disable
namespace SmartWinForms
{
    public partial class AIChat : UserControl
    {

        private readonly IChatCompletionService _chatService;
        private readonly ChatHistory _history;
        public AIChat()
        {
            InitializeComponent();
            var ollamaApiClient = new OllamaApiClient(new Uri("http://localhost:11434"), DefaultModelId);
            var builder = Kernel.CreateBuilder();
            builder.Services.AddScoped<IChatCompletionService>(_ => ollamaApiClient.AsChatCompletionService());
            var kernel = builder.Build();
            _chatService = kernel.GetRequiredService<IChatCompletionService>();
            _history = new ChatHistory();
            _history.AddSystemMessage(SystemPrompt);

        }
        public string SystemPrompt
        { get; set; } = "你是一个AI助理，用简练的语言回答问题";
        public string DefaultModelId
        {
            get;
            set;
        } = "phi4-mini";

        private async void sendBut_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(chatTB.Text))
            {
                MessageBox.Show("请输入内容");
                return;
            }
            var input = chatTB.Text;
            var response = _chatService.GetStreamingChatMessageContentsAsync(input);
            var content = "";
            var role = AuthorRole.Assistant;
            _history.AddUserMessage(input);
            historyTB.Text += $"用户:\n{_history.Last().Content}";
            historyTB.Text += $"AI助理:\n";
            historyTB.Text=historyTB.Text.Trim();
            Task.Run(async () =>
            {
                await foreach (var message in response)
                {
                    this.Invoke(() =>
                    {
                        historyTB.Text += $"{message.Content}";
                    });
                    content += message.Content;
                    role = message.Role.Value;
                }
            });
            historyTB.Text += $"\n";
            chatTB.Text = string.Empty;
            _history.AddMessage(role, content);
        }

        private void chatTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // 阻止默认的换行行为
                sendBut.PerformClick(); // 模拟点击发送按钮
            }
        }
    }
}

