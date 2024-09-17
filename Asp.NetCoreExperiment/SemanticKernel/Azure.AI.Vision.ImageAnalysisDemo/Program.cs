using Azure.AI.Vision.ImageAnalysis;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using OpenAI.Embeddings;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Numerics;
using System.Security.Policy;


string subscriptionKey = File.ReadAllText("C://GPT/visionkey.txt");
string endpoint = "https://gswtestvision.cognitiveservices.azure.com/";

// 图片URL
string imageUrl1 = "https://raw.githubusercontent.com/axzxs2001/Asp.NetCoreExperiment/master/Asp.NetCoreExperiment/SemanticKernel/Azure.AI.Vision.ImageAnalysisDemo/A.png";
var imageUrl2 = "https://raw.githubusercontent.com/axzxs2001/Asp.NetCoreExperiment/master/Asp.NetCoreExperiment/SemanticKernel/Azure.AI.Vision.ImageAnalysisDemo/B.png";

// 调用提取图片特征的函数
var vectorize1 = await VectorizeImageAsync(imageUrl1);
var vectorize2 = await VectorizeText("故宫，蓝天，建筑");
var vectorize3 = await VectorizeImageAsync(imageUrl2);
// 计算两个向量的余弦相似度
Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
var similarity = GetCosineSimilarity(vectorize1.Vector, vectorize3.Vector);
Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
Console.WriteLine("相似度是：" + similarity);

double GetCosineSimilarity(double[] vector1, double[] vector2)
{
    double dotProduct = 0;
    int length = Math.Min(vector1.Length, vector2.Length);
    for (int i = 0; i < length; i++)
    {
        dotProduct += vector1[i] * vector2[i];
    }
    var magnitude1 = Math.Sqrt(vector1.Select(x => x * x).Sum());
    var magnitude2 = Math.Sqrt(vector2.Select(x => x * x).Sum());

    return dotProduct / (magnitude1 * magnitude2);
}
async Task<VectorResult> VectorizeText(string text)
{
    // 创建HttpClient实例
    using (HttpClient client = new HttpClient())
    {
        // 设置请求头
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
        // 请求URL
        string requestUrl = endpoint + "computervision/retrieval:vectorizeText?api-version=2024-02-01&model-version=2023-04-15";

        // 请求内容
        var content = new StringContent("{\"text\":\"" + text + "\"}");
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        // 发送POST请求
        HttpResponseMessage response = await client.PostAsync(requestUrl, content);
        // 处理响应
        var result = await response.Content.ReadAsStringAsync();

        return System.Text.Json.JsonSerializer.Deserialize<VectorResult>(result, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}


async Task<VectorResult> VectorizeImageAsync(string imageUrl)
{
    // 创建HttpClient实例
    using (HttpClient client = new HttpClient())
    {
        // 设置请求头
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
        // 请求URL
        string requestUrl = endpoint + "computervision/retrieval:vectorizeImage?api-version=2024-02-01&model-version=2023-04-15";
        // 请求内容
        var content = new StringContent("{\"url\":\"" + imageUrl + "\"}");
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        // 发送POST请求
        HttpResponseMessage response = await client.PostAsync(requestUrl, content);
        // 处理响应
        var result = await response.Content.ReadAsStringAsync();
        return System.Text.Json.JsonSerializer.Deserialize<VectorResult>(result, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}
public class VectorResult
{
    public double[] Vector { get; set; }

    public string ModelVersion { get; set; }
}