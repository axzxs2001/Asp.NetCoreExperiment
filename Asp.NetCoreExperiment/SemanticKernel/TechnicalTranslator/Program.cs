
using Microsoft.Playwright;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

Console.WriteLine("请输入一个Url");
var url = Console.ReadLine();
Console.WriteLine("开始获取原文……");
var (title, content) = await GetArticleAsync(url);
Console.WriteLine("获取取得原文成功！");
Console.WriteLine($"标题：{title}");
Console.WriteLine("正文");
Console.WriteLine(content);
Console.WriteLine("开始翻译……");
var translatorContent = await OpenAIChatSampleAsync(title, content);

await PublishArticleAsync(translatorContent);
Console.WriteLine($"转换发布成功：{url}");
Console.ReadLine();

(string title, string content) SplitArticle(string article)
{
    File.WriteAllText(Directory.GetCurrentDirectory() + $"/{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt", article, Encoding.UTF8);
    var arr = article.Split(new string[] { "标题：", "内容：", "タイトル：" }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    if (arr.Length != 2)
    {
        throw new Exception("拆解不正确");
    }
    var title = arr[0];
    var content = arr[1];
    return (title, content);
}

async Task<bool> PublishArticleAsync(string translatorContent)
{
    var (title, content) = SplitArticle(translatorContent);
    using var playwright = await Playwright.CreateAsync();
    await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
    var page = await browser.NewPageAsync();
    await page.GotoAsync("https://qiita.com/");

    await page.ClickAsync("a[href='/login?callback_action=login_or_signup&redirect_to=%2F&realm=qiita']");
    var userArr = File.ReadAllLines("C:/gpt/qiita_user.txt");
    await page.FillAsync("#identity", userArr[0]);
    await page.FillAsync("#password", userArr[1]);
    await page.ClickAsync("input[name='commit']");
    await page.GotoAsync("https://qiita.com/drafts/new");
    await page.FillAsync("input[placeholder='記事タイトル']", title);
    await page.FillAsync("input[placeholder='タグを入力してください。スペース区切りで5つまで入力できます。']", "C# .NET");
    await page.FillAsync("div[role='textbox']", content);
    await page.ClickAsync("button[class='style-1pme5r3']");

    Console.WriteLine("\r\n\r\n");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("是否要发布？");
    Console.ResetColor();
    if (Console.ReadLine().ToLower() == "y")
    {
        await page.ClickAsync("button[class='style-4bw89i']");
    }
    return true;
}
async Task<(string Title, string Content)> GetArticleAsync(string url)
{
    using var playwright = await Playwright.CreateAsync();
    await using var browser = await playwright.Chromium.LaunchAsync(/*new BrowserTypeLaunchOptions {  Headless = false  }*/);
    var page = await browser.NewPageAsync();
    await page.GotoAsync(url);
    var title = await page.Locator("#activity-name").InnerTextAsync();
    var locator = page.Locator("#js_content");
    var images = await locator.GetByRole(AriaRole.Img).ElementHandlesAsync();
    var imageList = new List<string>();
    foreach (var image in images)
    {
        var imgUrl = await image.GetAttributeAsync("data-src");
        imageList.Add(imgUrl);
    }
    var html = await locator.InnerHTMLAsync();
    var imgTagPattern = @"<img[^>]*>";
    html = Regex.Replace(html, imgTagPattern, "[图片]");
    await page.SetContentAsync("<div id='js_content'>" + html + "</div>");
    var content = await page.Locator("#js_content").InnerTextAsync();
    var reader = new StringReader(content);
    var index = 0;
    var contentBuilder = new StringBuilder();
    while (true)
    {
        var line = await reader.ReadLineAsync();
        if (!string.IsNullOrWhiteSpace(line.Trim()))
        {
            if (line.Trim() == "[图片]")
            {
                contentBuilder.AppendLine($"![alt 图片]({imageList[index]})");
                index++;
            }
            else
            {
                contentBuilder.AppendLine(line);
            }
        }
        if (reader.Peek() <= 0)
        {
            break;
        }
    }
    return (title, contentBuilder.ToString());
}

async Task<string> OpenAIChatSampleAsync(string title, string content)
{
    var key = File.ReadAllText(@"C:\GPT\key.txt");
    var chatModelId = "gpt-4-0125-preview";
    OpenAIChatCompletionService chatCompletionService = new(chatModelId, key);
    return await StartChatAsync(chatCompletionService, title, content);
}

async Task<string> StartChatAsync(IChatCompletionService chatGPT, string title, string content)
{
    var prompt = File.ReadAllText(Environment.CurrentDirectory + "/Prompt.md");
    var chatHistory = new ChatHistory(prompt);
    var userContent = $"标题：{title}\r\n内容：{content}";
    chatHistory.AddUserMessage(userContent);
    //var result = await chatGPT.GetChatMessageContentAsync(chatHistory);
    //Console.WriteLine(result.Content);
    return await MessageStreamOutputAsync(chatGPT, chatHistory);
}

static async Task<string> MessageStreamOutputAsync(IChatCompletionService chatGPT, ChatHistory chatHistory)
{
    var list = chatGPT.GetStreamingChatMessageContentsAsync(chatHistory);
    var first = true;
    AuthorRole? role = AuthorRole.Assistant;
    var fullMessage = string.Empty;
    await foreach (var item in list)
    {
        if (first)
        {
            role = item.Role;
            first = false;
            Console.Write($"{role}：");
        }
        if (item == null)
        {
            continue;
        }
        fullMessage += item.Content;
        Console.Write($"{item.Content}");
    }
    return fullMessage;
}
