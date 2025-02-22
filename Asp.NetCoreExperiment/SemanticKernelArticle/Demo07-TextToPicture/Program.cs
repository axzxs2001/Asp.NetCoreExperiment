﻿using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.TextToImage;
using System.Diagnostics;
using System.Xml.Linq;

Console.WriteLine("开始生成……");
#pragma warning disable SKEXP0010
var key = File.ReadAllText(@"C:\GPT\key.txt");
var kernel = Kernel.CreateBuilder()
    .AddOpenAITextToImage(key,modelId: "dall-e-3")
    .Build();
var prompt1 = $"背景是白色，用墨水，画一匹腾空跃起的骏马。要求马位中图的中央，显示完整。";
await CreateImageAsync(prompt1);

async Task CreateImageAsync(string prompt)
{
#pragma warning disable SKEXP0001
    var dallE = kernel.GetRequiredService<ITextToImageService>();
    var imageUrl = await dallE.GenerateImageAsync(prompt, 1024, 1792);
    await DownLoadImageAsync(imageUrl);
}
async Task DownLoadImageAsync(string imageUrl)
{
    var localPath = "downloaded_image.jpg";
    using (var client = new HttpClient())
    {
        var response = await client.GetAsync(imageUrl);
        response.EnsureSuccessStatusCode();
        var imageBytes = await response.Content.ReadAsByteArrayAsync();
        await File.WriteAllBytesAsync(localPath, imageBytes);
        Console.WriteLine("图片下载成功，保存在：" + localPath);
    }
    Process.Start(new ProcessStartInfo(localPath) { UseShellExecute = true });
}