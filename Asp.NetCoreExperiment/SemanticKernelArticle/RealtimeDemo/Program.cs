// 根据可用的 OpenAI 或 Azure OpenAI 配置检索 RealtimeConversationClient。
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using NAudio.Wave;
using OpenAI.RealtimeConversation;
using System.ClientModel;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Text;
using System.Text.Json;

#pragma warning disable
// 1. 定义队列
var audioQueue = new BlockingCollection<byte[]>(boundedCapacity: 100);

var waveOut = new WaveOutEvent();
var waveProvider = new BufferedWaveProvider(new WaveFormat(24000, 16, 1));
//waveProvider.BufferLength = 100000;
waveOut.Init(waveProvider);
waveOut.Play();

var playbackTask = Task.Run(() =>
{
    foreach (var buffer in audioQueue.GetConsumingEnumerable())
    {
        while (waveProvider.BufferedBytes + buffer.Length > waveProvider.BufferLength)
        {
            Thread.Sleep(10); // 等待缓冲区有空间
        }
        waveProvider.AddSamples(buffer, 0, buffer.Length);
    }
});




var key = File.ReadAllText("C:/gpt/key.txt");
var realtimeConversationClient = new RealtimeConversationClient(
            model: "gpt-4o-realtime-preview",
            credential: new ApiKeyCredential(key));

// 构建 Kernel。
var kernel = Kernel.CreateBuilder().Build();

// 导入插件。
kernel.ImportPluginFromType<WeatherPlugin>();

// 开始新的会话。
using RealtimeConversationSession session = await realtimeConversationClient.StartConversationSessionAsync();

// 初始化会话选项。
// 会话选项控制所有会话中共享的连接行为，包括音频输入格式和语音活动检测设置。
ConversationSessionOptions sessionOptions = new()
{
    Voice = ConversationVoice.Alloy,
    InputAudioFormat = ConversationAudioFormat.Pcm16,
    OutputAudioFormat = ConversationAudioFormat.Pcm16,
    InputTranscriptionOptions = new()
    {
        Model = "whisper-1",
    },
};

// 将 Kernel 中的插件/函数添加为会话工具。
foreach (var tool in ConvertFunctions(kernel))
{
    sessionOptions.Tools.Add(tool);
}

// 如果有可用的工具，设置工具选择为“auto”（自动选择）。
if (sessionOptions.Tools.Count > 0)
{
    sessionOptions.ToolChoice = ConversationToolChoice.CreateAutoToolChoice();
}

// 使用定义的选项配置会话。
await session.ConfigureSessionAsync(sessionOptions);

// 可以向会话发送用户、助手或系统消息，以及输入音频。
// 示例：发送用户消息到会话。
// ConversationItem 可以根据 Microsoft.SemanticKernel.ChatMessageContent 构造，映射相关字段。
await session.AddItemAsync(
    ConversationItem.CreateUserMessage(["我在考虑出行时穿什么。"]));

// 使用包含录音问题的音频文件："旧金山，加利福尼亚的天气如何？"
string inputAudioPath = FindFile("realtime_whats_the_weather_pcm16_24khz_mono.wav");
using Stream inputAudioStream = File.OpenRead(inputAudioPath);

// 示例：将输入音频发送到会话。
await session.SendInputAudioAsync(inputAudioStream);

// 初始化字典以存储流式音频响应和函数参数。
Dictionary<string, MemoryStream> outputAudioStreamsById = [];
Dictionary<string, StringBuilder> functionArgumentBuildersById = [];

// 定义循环以接收会话中的对话更新。
await foreach (ConversationUpdate update in session.ReceiveUpdatesAsync())
{

    //可以中断输出  session.CancelResponseAsync
    // 通知：对话会话开始。
    if (update is ConversationSessionStartedUpdate sessionStartedUpdate)
    {
        Console.WriteLine($"<<< 会话已开始。ID: {sessionStartedUpdate.SessionId}");
        Console.WriteLine();
    }

    // 通知：检测到语音活动开始。
    if (update is ConversationInputSpeechStartedUpdate speechStartedUpdate)
    {
        Console.WriteLine(
            $"  -- 语音活动检测开始于 {speechStartedUpdate.AudioStartTime}");
    }

    // 通知：检测到语音活动结束。
    if (update is ConversationInputSpeechFinishedUpdate speechFinishedUpdate)
    {
        Console.WriteLine(
            $"  -- 语音活动检测结束于 {speechFinishedUpdate.AudioEndTime}");
    }

    // 通知：开始流式传输条目，如函数调用或响应消息。
    if (update is ConversationItemStreamingStartedUpdate itemStreamingStartedUpdate)
    {
        Console.WriteLine("  -- 开始新条目的流式传输");
        if (!string.IsNullOrEmpty(itemStreamingStartedUpdate.FunctionName))
        {
            Console.Write($"    {itemStreamingStartedUpdate.FunctionName}: ");
        }
    }

    // 通知：关于流式传输的部分更新，可能包含音频转录、音频字节或函数参数。
    if (update is ConversationItemStreamingPartDeltaUpdate deltaUpdate)
    {
        Console.Write(deltaUpdate.AudioTranscript);
        Console.Write(deltaUpdate.Text);
        Console.Write(deltaUpdate.FunctionArguments);

        // 处理音频字节。
        if (deltaUpdate.AudioBytes is not null)
        {
            if (!outputAudioStreamsById.TryGetValue(deltaUpdate.ItemId, out MemoryStream? value))
            {
                value = new MemoryStream();
                outputAudioStreamsById[deltaUpdate.ItemId] = value;
            }
            audioQueue.Add(deltaUpdate.AudioBytes.ToArray());
            value.Write(deltaUpdate.AudioBytes);
        }

        // 处理函数参数。
        if (!functionArgumentBuildersById.TryGetValue(deltaUpdate.ItemId, out StringBuilder? arguments))
        {
            functionArgumentBuildersById[deltaUpdate.ItemId] = arguments = new();
        }

        if (!string.IsNullOrWhiteSpace(deltaUpdate.FunctionArguments))
        {
            arguments.Append(deltaUpdate.FunctionArguments);
        }
    }

    // 通知：条目流式传输结束，如函数调用或响应消息。
    // 此时，可以在控制台上显示音频转录结果，或使用聚合参数调用函数。
    if (update is ConversationItemStreamingFinishedUpdate itemStreamingFinishedUpdate)
    {
        Console.WriteLine();
        Console.WriteLine($"  -- 条目流结束, item_id={itemStreamingFinishedUpdate.ItemId}");

        // 如果是函数调用条目，则调用对应函数。
        if (itemStreamingFinishedUpdate.FunctionCallId is not null)
        {
            Console.WriteLine($"    + 响应由此条目调用的工具: {itemStreamingFinishedUpdate.FunctionName}");

            // 解析函数名称。
            var (functionName, pluginName) = ParseFunctionName(itemStreamingFinishedUpdate.FunctionName);

            // 反序列化参数。
            var argumentsString = functionArgumentBuildersById[itemStreamingFinishedUpdate.ItemId].ToString();
            var arguments = DeserializeArguments(argumentsString);

            // 创建函数调用内容。
            var functionCallContent = new FunctionCallContent(
                functionName: functionName,
                pluginName: pluginName,
                id: itemStreamingFinishedUpdate.FunctionCallId,
                arguments: arguments);

            // 调用函数。
            var resultContent = await functionCallContent.InvokeAsync(kernel);

            // 创建包含函数调用结果的对话条目并发送回会话。
            ConversationItem functionOutputItem = ConversationItem.CreateFunctionCallOutput(
                callId: itemStreamingFinishedUpdate.FunctionCallId,
                output: ProcessFunctionResult(resultContent.Result));

            await session.AddItemAsync(functionOutputItem);
        }
        // 如果是响应消息条目，输出到控制台。
        else if (itemStreamingFinishedUpdate.MessageContentParts?.Count > 0)
        {
            Console.Write($"    + [{itemStreamingFinishedUpdate.MessageRole}]: ");

            foreach (ConversationContentPart contentPart in itemStreamingFinishedUpdate.MessageContentParts)
            {
                Console.Write(contentPart.AudioTranscript);
            }

            Console.WriteLine();
        }
    }

    // 通知：输入音频的转录完成。
    if (update is ConversationInputTranscriptionFinishedUpdate transcriptionCompletedUpdate)
    {
        Console.WriteLine();
        Console.WriteLine($"  -- 用户音频转录内容: {transcriptionCompletedUpdate.Transcript}");
        Console.WriteLine();
    }

    // 通知：模型响应回合完成。
    if (update is ConversationResponseFinishedUpdate turnFinishedUpdate)
    {
        Console.WriteLine($"  -- 模型生成响应结束，状态: {turnFinishedUpdate.Status}");

        // 如果新创建的条目包含函数名，表示函数调用结果已提供，可以开始响应。
        if (turnFinishedUpdate.CreatedItems.Any(item => item.FunctionName?.Length > 0))
        {
            Console.WriteLine("  -- 结束当前客户端回合，等待工具响应");
            await session.StartResponseAsync();
        }
        // 否则，模型响应已提供，可结束更新。
        else
        {
            break;
        }
    }

    // 会话中出现错误的通知。
    if (update is ConversationErrorUpdate errorUpdate)
    {
        Console.WriteLine();
        Console.WriteLine($"错误: {errorUpdate.Message}");
        break;
    }
}

// 输出接收到的音频数据大小并释放流。
var outArr = new List<byte>();
foreach ((string itemId, Stream outputAudioStream) in outputAudioStreamsById)
{
    Console.WriteLine($"音频输出 {itemId}: {outputAudioStream.Length} 字节");
    outputAudioStream.Seek(0, SeekOrigin.Begin);
    using (var memoryStream = new MemoryStream())
    {
        outputAudioStream.CopyTo(memoryStream);
        outArr.AddRange(memoryStream.ToArray());
    }
    outputAudioStream.Dispose();
}

// 写入带WAV头的文件（16bit PCM, 24kHz, 单声道）
WriteWavFile("realtime_whats_the_weather_pcm16_24khz_mono_out.wav", outArr.ToArray(), 24000, 1, 16);

/// <summary>写入带WAV头的PCM音频文件</summary>
static void WriteWavFile(string filePath, byte[] pcmData, int sampleRate, short channels, short bitsPerSample)
{
    using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
    using var bw = new BinaryWriter(fs);
    int byteRate = sampleRate * channels * bitsPerSample / 8;
    short blockAlign = (short)(channels * bitsPerSample / 8);
    int subchunk2Size = pcmData.Length;
    int chunkSize = 36 + subchunk2Size;

    // RIFF header
    bw.Write(Encoding.ASCII.GetBytes("RIFF"));
    bw.Write(chunkSize);
    bw.Write(Encoding.ASCII.GetBytes("WAVE"));

    // fmt subchunk
    bw.Write(Encoding.ASCII.GetBytes("fmt "));
    bw.Write(16); // Subchunk1Size for PCM
    bw.Write((short)1); // AudioFormat = PCM
    bw.Write(channels);
    bw.Write(sampleRate);
    bw.Write(byteRate);
    bw.Write(blockAlign);
    bw.Write(bitsPerSample);

    // data subchunk
    bw.Write(Encoding.ASCII.GetBytes("data"));
    bw.Write(subchunk2Size);
    bw.Write(pcmData);
}


audioQueue.CompleteAdding();
await playbackTask;
//waveOut.Dispose();
Console.ReadLine();

/// <summary>Helper method to parse a function name for compatibility with Semantic Kernel plugins/functions.</summary>
static (string FunctionName, string? PluginName) ParseFunctionName(string fullyQualifiedName)
{
    const string FunctionNameSeparator = "-";

    string? pluginName = null;
    string functionName = fullyQualifiedName;

    int separatorPos = fullyQualifiedName.IndexOf(FunctionNameSeparator, StringComparison.Ordinal);
    if (separatorPos >= 0)
    {
        pluginName = fullyQualifiedName.AsSpan(0, separatorPos).Trim().ToString();
        functionName = fullyQualifiedName.AsSpan(separatorPos + FunctionNameSeparator.Length).Trim().ToString();
    }

    return (functionName, pluginName);
}

/// <summary>Helper method to deserialize function arguments.</summary>
static KernelArguments? DeserializeArguments(string argumentsString)
{
    var arguments = JsonSerializer.Deserialize<KernelArguments>(argumentsString);

    if (arguments is not null)
    {
        // Iterate over copy of the names to avoid mutating the dictionary while enumerating it
        var names = arguments.Names.ToArray();
        foreach (var name in names)
        {
            arguments[name] = arguments[name]?.ToString();
        }
    }

    return arguments;
}

/// <summary>Helper method to process function result in order to provide it to the model as string.</summary>
static string? ProcessFunctionResult(object? functionResult)
{
    if (functionResult is string stringResult)
    {
        return stringResult;
    }

    return JsonSerializer.Serialize(functionResult);
}

/// <summary>Helper method to convert Kernel plugins/function to realtime session conversation tools.</summary>
static IEnumerable<ConversationTool> ConvertFunctions(Kernel kernel)
{
    foreach (var plugin in kernel.Plugins)
    {
        var functionsMetadata = plugin.GetFunctionsMetadata();

        foreach (var metadata in functionsMetadata)
        {
            var toolDefinition = metadata.ToOpenAIFunction().ToFunctionDefinition(false);

            yield return new ConversationFunctionTool(name: toolDefinition.FunctionName)
            {
                Description = toolDefinition.FunctionDescription,
                Parameters = toolDefinition.FunctionParameters
            };
        }
    }
}

/// <summary>Helper method to get a file path.</summary>
static string FindFile(string fileName)
{
    for (string currentDirectory = Directory.GetCurrentDirectory();
         currentDirectory != null && currentDirectory != Path.GetPathRoot(currentDirectory);
         currentDirectory = Directory.GetParent(currentDirectory)?.FullName!)
    {
        string filePath = Path.Combine(currentDirectory, fileName);
        if (File.Exists(filePath))
        {
            return filePath;
        }
    }

    throw new FileNotFoundException($"File '{fileName}' not found.");
}

/// <summary>A sample plugin to get a weather.</summary>
class WeatherPlugin
{
    [KernelFunction]
    [Description("Gets the current weather for the specified city in Fahrenheit.")]
    public static string GetWeatherForCity([Description("City name without state/country.")] string cityName)
    {
        return cityName switch
        {
            "Boston" => "61 and rainy",
            "London" => "55 and cloudy",
            "Miami" => "80 and sunny",
            "Paris" => "60 and rainy",
            "Tokyo" => "50 and sunny",
            "Sydney" => "75 and sunny",
            "Tel Aviv" => "80 and sunny",
            "San Francisco" => "70 and sunny",
            _ => throw new ArgumentException($"Data is not available for {cityName}."),
        };
    }
}
