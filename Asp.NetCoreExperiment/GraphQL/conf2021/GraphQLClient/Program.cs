
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System.Text.RegularExpressions;

Console.WriteLine("回车开始：");
Console.ReadLine();
var client = new HttpClient();

var json = System.Text.Json.JsonSerializer.Serialize(new Entity { query = @"{
  __type(name:""ExamPaper""){
    name
    fields {
        name
      description
    }
}
}" });
var content = new StringContent(json);
//Console.WriteLine(json);
content.Headers.Clear();
content.Headers.Add("Content-Type", "application/json;charset=utf-8");
var response = await client.PostAsync("http://localhost:5147/graphql/", content);

var backContent = await response.Content.ReadAsStringAsync();
var backJson = Regex.Unescape(backContent);
//Console.WriteLine(backJson);
var data = System.Text.Json.JsonSerializer.Deserialize<BackData>(backJson);
var direction = new Dictionary<string, string>();
foreach (var item in data.data.__type.fields)
{
    if (string.IsNullOrWhiteSpace(item.description))
    {
        direction.Add(item.name, item.name);
    }
    else
    {
        direction.Add(item.description, item.name);
    }
}
Console.WriteLine("请语音选择下列字段（如 请说【添加编号】）：");
Console.WriteLine("-----------------");
foreach (var keyvalue in direction)
{
    Console.WriteLine($"添加{keyvalue.Key}");
}
Console.WriteLine("-----------------");

json = "";
while (true)
{
    var key = System.IO.File.ReadAllText(@"C:\NetStars\speechkey.txt");
    var speechConfig = SpeechConfig.FromSubscription(key, "japaneast");
    speechConfig.SpeechRecognitionLanguage = "zh-CN";
    var text = await FromMic(speechConfig);

    if (text.Contains("添加"))
    {
        foreach (var keyvalue in direction)
        {
            if (text.Replace("添加", "").Contains(keyvalue.Key))
            {
                json += keyvalue.Value + ",";
            }
        }
    }
    if (text.Contains("完成"))
    {
        break;
    }
}
content = new StringContent(@$"{{""query"":""{{examPaper{{ {json.TrimEnd(',')}}}}}""}}");
content.Headers.Clear();
content.Headers.Add("Content-Type", "application/json;charset=utf-8");
response = await client.PostAsync("http://localhost:5147/graphql/", content);

backContent = await response.Content.ReadAsStringAsync();
backJson = Regex.Unescape(backContent);
var queryData = System.Text.Json.JsonSerializer.Deserialize<BacckQuery>(backJson);
Console.WriteLine("=========================================");
foreach (var exam in queryData.data.examPaper)
{
    Console.WriteLine($"| {exam.id} |  {exam.title}| {exam.scores}| {exam.count}  |{exam.createTime}| {exam.memo}");
}
Console.WriteLine("=========================================");
//Console.WriteLine(backJson);
Console.WriteLine("查询完成");
Console.ReadLine();




async static Task<string> FromMic(SpeechConfig speechConfig)
{
    using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
    using var recognizer = new SpeechRecognizer(speechConfig, audioConfig);
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("请讲：");
    Console.ResetColor();
    var result = await recognizer.RecognizeOnceAsync();
    Console.WriteLine($"你说的是：{result.Text}");
    return result.Text;
}



class Entity
{
    public string query { get; set; }
}

public class Field
{
    public string name { get; set; }
    public string description { get; set; }
}
public class DataType
{
    public string name { get; set; }
    public Field[] fields { get; set; }
}

public class Data
{
    public DataType __type { get; set; }
}

public class BackData
{
    public Data data { get; set; }
}

public class BacckQuery
{
    public QueryData data { get; set; }
}
public class QueryData
{
    public ExamPaper[] examPaper { get; set; }
}
public class ExamPaper
{
    public int id { get; set; }
    public string title { get; set; } = null!;
    public string? memo { get; set; }
    public DateTime createTime { get; set; }

    public double scores { get; set; }
    public int count { get; set; }
}