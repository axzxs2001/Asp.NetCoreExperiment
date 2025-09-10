



using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


await ImageToImageAsync();
//await TextToImageAsync();
async Task TextToImageAsync()
{

    // 把你的 API Key 放到环境变量 GEMINI_API_KEY，或直接写死（不推荐）
    string ApiKey = File.ReadAllLines("C:/gpt/googlecloudkey.txt")[0];
    string Endpoint =
       "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash-image-preview:generateContent";


    var prompt = "在柔和的晨光下，在木桌上生成逼真的西瓜";

    using var http = new HttpClient();
    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    var requestBody = new
    {
        contents = new[]
        {
                new
                {
                    role = "user",
                    parts = new object[]
                    {
                        new { text = prompt }
                    }
                }
            }
    };

    var url = $"{Endpoint}?key={ApiKey}";
    var json = JsonSerializer.Serialize(requestBody);
    using var resp = await http.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
    resp.EnsureSuccessStatusCode();

    using var stream = await resp.Content.ReadAsStreamAsync();
    using var doc = await JsonDocument.ParseAsync(stream);

    // 提取返回里的 inline_data（图片二进制，base64），并保存为 PNG
    var candidates = doc.RootElement.GetProperty("candidates");
    int imageIndex = 0;

    foreach (var cand in candidates.EnumerateArray())
    {
        var parts = cand.GetProperty("content").GetProperty("parts");
        foreach (var part in parts.EnumerateArray())
        {
            if (part.TryGetProperty("inlineData", out var inline))
            {
                var b64 = inline.GetProperty("data").GetString();
                var bytes = Convert.FromBase64String(b64!);
                var file = $"output_{imageIndex++:00}.png";
                await File.WriteAllBytesAsync(file, bytes);
                Console.WriteLine($"Saved: {file}");
            }
            // 该模型也可能返回文本（例如解释/提示），可按需处理 part.text
        }
    }
}

async Task ImageToImageAsync()
{
    string ApiKey = File.ReadAllText("C:/gpt/googlecloudkey.txt");
    string Endpoint =
       "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash-image-preview:generateContent";
    // 准备两张输入图：前景人物 + 海滩背景
    var fgBytes = await File.ReadAllBytesAsync("person.jpg");
    var bgBytes = await File.ReadAllBytesAsync("beach.png");
    string fgB64 = Convert.ToBase64String(fgBytes);
    string bgB64 = Convert.ToBase64String(bgBytes);

    var instruction = "将人物自然融入海滩场景中，人物不要太大，侧面，注意匹配光影效果，并保持肤色真实自然。";

    var requestBody = new
    {
        contents = new[]
        {
                new
                {
                    role = "user",
                    parts = new object[]
                    {
                        // 第一张输入图
                        new {
                            inline_data = new {
                                mime_type = "image/png",
                                data = fgB64
                            }
                        },
                        // 第二张输入图
                        new {
                            inline_data = new {
                                mime_type = "image/jpeg",
                                data = bgB64
                            }
                        },
                        // 编辑指令
                        new { text = instruction }
                    }
                }
            }
    };

    var url = $"{Endpoint}?key={ApiKey}";

    using var http = new HttpClient();
    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    var json = JsonSerializer.Serialize(requestBody);
    using var resp = await http.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
    resp.EnsureSuccessStatusCode();

    using var stream = await resp.Content.ReadAsStreamAsync();
    using var doc = await JsonDocument.ParseAsync(stream);

    int imageIndex = 0;
    foreach (var cand in doc.RootElement.GetProperty("candidates").EnumerateArray())
    {
        foreach (var part in cand.GetProperty("content").GetProperty("parts").EnumerateArray())
        {
            if (part.TryGetProperty("inlineData", out var inline))
            {
                var b64 = inline.GetProperty("data").GetString();
                var bytes = Convert.FromBase64String(b64!);
                var file = $"edited_{DateTime.Now.ToString("yyyyMMddHHmmss")}.png";
                await File.WriteAllBytesAsync(file, bytes);
                Console.WriteLine($"Saved: {file}");
            }
        }
    }
}

async Task ImageToImageAsync1()
{
    string ApiKey = File.ReadAllLines("C:/gpt/googlecloudkey.txt")[0];
    string Endpoint =
       "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash-image-preview:generateContent";
    // 准备两张输入图：前景人物 + 海滩背景
    var fgBytes = await File.ReadAllBytesAsync("person.jpg");

    string fgB64 = Convert.ToBase64String(fgBytes);
  

    var instruction = "根据用户提供的照片，转换成一张证件照，要求白底，人物清晰。";

    var requestBody = new
    {
        contents = new[]
        {
                new
                {
                    role = "user",
                    parts = new object[]
                    {
                        // 第一张输入图
                        new {
                            inline_data = new {
                                mime_type = "image/png",
                                data = fgB64
                            }
                        },          
                        // 编辑指令
                        new { text = instruction }
                    }
                }
            }
    };

    var url = $"{Endpoint}?key={ApiKey}";

    using var http = new HttpClient();
    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    var json = JsonSerializer.Serialize(requestBody);
    using var resp = await http.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
    resp.EnsureSuccessStatusCode();

    using var stream = await resp.Content.ReadAsStreamAsync();
    using var doc = await JsonDocument.ParseAsync(stream);

    int imageIndex = 0;
    foreach (var cand in doc.RootElement.GetProperty("candidates").EnumerateArray())
    {
        foreach (var part in cand.GetProperty("content").GetProperty("parts").EnumerateArray())
        {
            if (part.TryGetProperty("inlineData", out var inline))
            {
                var b64 = inline.GetProperty("data").GetString();
                var bytes = Convert.FromBase64String(b64!);
                var file = $"edited_{DateTime.Now.ToString("yyyyMMddHHmmss")}.png";
                await File.WriteAllBytesAsync(file, bytes);
                Console.WriteLine($"Saved: {file}");
            }
        }
    }
}