﻿using Azure.AI.Vision.ImageAnalysis;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using OpenAI.Embeddings;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Numerics;
using System.Security.Policy;
using System.Text.Json;

string subscriptionKey = File.ReadAllText("C://GPT/visionkey.txt");
string endpoint = "https://gswtestvision.cognitiveservices.azure.com/";

// 图片URL
var imageUrl1 = "https://raw.githubusercontent.com/axzxs2001/Asp.NetCoreExperiment/master/Asp.NetCoreExperiment/SemanticKernel/Azure.AI.Vision.ImageAnalysisDemo/A.png";
var imageUrl2 = "https://raw.githubusercontent.com/axzxs2001/Asp.NetCoreExperiment/master/Asp.NetCoreExperiment/SemanticKernel/Azure.AI.Vision.ImageAnalysisDemo/B.png";
var imageUrl3 = "https://raw.githubusercontent.com/axzxs2001/Asp.NetCoreExperiment/master/Asp.NetCoreExperiment/SemanticKernel/Azure.AI.Vision.ImageAnalysisDemo/C.png";
// 调用提取图片特征的函数
Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
var vectorize0 = await VectorizeText("微糖，深味，棕色，Wondn，金色盖子");
var vectorizeA = await VectorizeImageAsync(imageUrl1);
var vectorizeB = await VectorizeImageAsync(imageUrl2);
var vectorizeC = await VectorizeImageAsync(imageUrl3);
Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));


// 计算两个向量的余弦相似度
var similarityAB = GetCosineSimilarity(vectorizeA.Vector, vectorizeB.Vector);
Console.WriteLine("A和B相似度是：" + similarityAB);
var similarityAC = GetCosineSimilarity(vectorizeA.Vector, vectorizeC.Vector);
Console.WriteLine("A和C相似度是：" + similarityAC);
var similarityBC = GetCosineSimilarity(vectorizeB.Vector, vectorizeC.Vector);
Console.WriteLine("B和C相似度是：" + similarityBC);
Console.WriteLine("===================================");
var similarityA0 = GetCosineSimilarity(vectorizeA.Vector, vectorize0.Vector);
Console.WriteLine("A和0相似度是：" + similarityA0);
var similarityB0 = GetCosineSimilarity(vectorizeB.Vector, vectorize0.Vector);
Console.WriteLine("B和0相似度是：" + similarityB0);
var similarityC0 = GetCosineSimilarity(vectorizeC.Vector, vectorize0.Vector);
Console.WriteLine("C和0相似度是：" + similarityC0);



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
    using (var client = new HttpClient())
    {
        // 设置请求头
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
        // 请求URL
        var requestUrl = endpoint + "computervision/retrieval:vectorizeText?api-version=2024-02-01&model-version=2023-04-15";

        // 请求内容
        var content = new StringContent("{\"text\":\"" + text + "\"}");
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        // 发送POST请求
        var response = await client.PostAsync(requestUrl, content);
        // 处理响应
        var result = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<VectorResult>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}


async Task<VectorResult> VectorizeImageAsync(string imageUrl)
{
    // 创建HttpClient实例
    using (var client = new HttpClient())
    {
        // 设置请求头
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
        // 请求URL
        var requestUrl = endpoint + "computervision/retrieval:vectorizeImage?api-version=2024-02-01&model-version=2023-04-15";
        // 请求内容
        var content = new StringContent("{\"url\":\"" + imageUrl + "\"}");
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        // 发送POST请求
        var response = await client.PostAsync(requestUrl, content);
        // 处理响应
        var result = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<VectorResult>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}
public class VectorResult
{
    public double[] Vector { get; set; }

    public string ModelVersion { get; set; }
}