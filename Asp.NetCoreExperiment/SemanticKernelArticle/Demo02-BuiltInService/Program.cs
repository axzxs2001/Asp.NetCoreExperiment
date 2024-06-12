using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Text.Json;

var chatModelId = "gpt-3.5-turbo-instruct";
var key = File.ReadAllText(@"C:\GPT\key.txt");

var textGenerationService = new OpenAITextGenerationService(chatModelId, key);
Console.WriteLine("---------------非流式---------------");
/*
这些参数是用来精细控制OpenAI GPT模型在文本生成过程中的行为的：
max_tokens:这个参数定义了模型输出的最大词数（或者说是token数）。Token不仅仅是单词，还包括标点符号和空格等。这个限制帮助控制生成内容的长度。
temperature:这个参数用于控制输出的随机性或者创造性。温度值在0到1之间，较低的温度（如0.2或0.3）会让模型的输出更加可预测和稳定，而较高的温度（如0.8或1）会增加输出的随机性和多样性，但也可能导致文本的连贯性和相关性下降。
top_p (Nucleus Sampling):这个参数控制模型在选择下一个词时考虑的范围。例如，如果top_p设置为0.9，模型将只从概率累积到90%的那部分词中选择下一个词。这通常有助于保持文本的相关性同时还能保持一定的创意自由。
presence_penalty 和 frequency_penalty:这两个参数用于增加输出的多样性和降低重复性。presence_penalty增加了已出现过的词再次出现的代价，有助于避免重复同一主题或词汇。frequency_penalty类似，但它是基于词出现的频率来增加代价，频繁出现的词在后续生成中被选中的概率将降低。
这些参数的组合可以帮助调整生成文本的风格和质量，以适应不同的应用场景和需求。
 */
//var settings = new PromptExecutionSettings
//{
//    ExtensionData = new Dictionary<string, object>
//    {
//        ["max_tokens"] = 1000,
//        ["temperature"] = 0.2,
//        ["top_p"] = 0.8,
//        ["presence_penalty"] = 0.0,
//        ["frequency_penalty"] = 0.0
//    }
//};

//var textContents = await textGenerationService.GetTextContentsAsync("用50个字描述一下.NET", settings);
//foreach (var textContent in textContents)
//{
//    var usage = textContent?.Metadata?["Usage"] as Azure.AI.OpenAI.CompletionsUsage;
//    if (usage != null)
//    {
//        var tokenStr = @$"====================Tokens==================
//提示词Tokens数：{usage.PromptTokens}
//返回内容Tokens数：{usage.CompletionTokens}
//总Tokens数：{usage.TotalTokens}
//===========================================";
//        Console.WriteLine(tokenStr);
//    }
//    Console.WriteLine(textContent.Text);
//}

//Console.WriteLine("---------------流式---------------");
//var streamTextContents = textGenerationService.GetStreamingTextContentsAsync("用50个字描述一下C#");
//await foreach (var textContent in streamTextContents)
//{
//    Console.Write(textContent.Text);
//}


chatModelId = "gpt-4o";
//var chatCompletionService = new OpenAIChatCompletionService(chatModelId, key);
//var messageContents = await chatCompletionService.GetChatMessageContentsAsync("用50个字描述一下.NET");
//foreach (var messageContent in messageContents)
//{
//    var usage = messageContent?.Metadata?["Usage"] as Azure.AI.OpenAI.CompletionsUsage;
//    if (usage != null)
//    {
//        var tokenStr = @$"
//====================Tokens==================
//提示词Tokens数：{usage.PromptTokens}
//返回内容Tokens数：{usage.CompletionTokens}
//总Tokens数：{usage.TotalTokens}
//===========================================";
//        Console.WriteLine(tokenStr);
//    }
//    Console.WriteLine(messageContent?.Content);
//}


var chatCompletionService = new OpenAIChatCompletionService(chatModelId, key);
var chatHistory = new ChatHistory("你是一位.NET专家，有深厚的.NET知识。");
var userMessage = "用50个字描述一下.NET";
Console.WriteLine("用户："+userMessage);
chatHistory.AddUserMessage(userMessage);
var messageContents = await chatCompletionService.GetChatMessageContentsAsync(chatHistory);
foreach (var messageContent in messageContents)
{
    chatHistory.AddAssistantMessage(messageContent?.Content);
    Console.WriteLine("专家：" + messageContent?.Content);
}
userMessage = "还有补充吗？";
Console.WriteLine("用户：" + userMessage);
chatHistory.AddUserMessage(userMessage);
messageContents = await chatCompletionService.GetChatMessageContentsAsync(chatHistory);
foreach (var messageContent in messageContents)
{
    chatHistory.AddAssistantMessage(messageContent?.Content);
    Console.WriteLine("专家：" + messageContent?.Content);
}

#pragma warning disable SKEXP0010
//var textToImageService = new OpenAITextToImageService(key);
//var imageUrl = await textToImageService.GenerateImageAsync("画一个科技公司网站404的报错图", 1024, 1024);
//Console.WriteLine(imageUrl);

/*
OpenAITextToAudioService
OpenAITextEmbeddingGenerationService
OpenAIFileService
OpenAIChatCompletionService
OpenAIAudioToTextService
*/
Console.ReadLine();