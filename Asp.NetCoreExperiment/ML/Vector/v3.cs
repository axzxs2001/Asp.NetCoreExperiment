using Dapper;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using NetTopologySuite.Index.HPRtree;
using Npgsql;
using OllamaSharp.Models;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Text.Json;

namespace Vector;

public static class v3
{
    /// <summary>
    /// 使用 ONNX 模型将文本生成向量
    /// </summary>
    public static void Example(List<Job> jobs)
    {
        var modelPath = @"C:\GPT\ONNX\jina-embeddings-v3\onnx\model.onnx";
        var tokenizerPath = @"C:\GPT\ONNX\jina-embeddings-v3\tokenizer.json";
        var vectorGenerator = new TextVectorGenerator(modelPath, tokenizerPath);

        //foreach (var job in jobs)
        //{
        //    job.DescriptionEmbedding = vectorGenerator.GenerateVector(job.Description);
        //    using (IDbConnection db = new NpgsqlConnection(File.ReadAllText("C://GPT/just-agi-db.txt")))
        //    {
        //        string sqlQuery = @"
        //        INSERT INTO public.imagevector1 (name, embedding,createtime) 
        //        VALUES (@Name, @Embedding,@CreateTime) 
        //        RETURNING id;";
        //        var ds = new List<double>();
        //        foreach (var item in job.DescriptionEmbedding.Value.ToArray())
        //        {
        //            ds.Add((double)item);
        //        }
        //        var parameters = new
        //        {
        //            Name = job.JobTitle,
        //            Embedding = ds.AsReadOnly<double>(),
        //            CreateTime = DateTime.Now
        //        };
        //        var id = db.ExecuteScalar<int>(sqlQuery, parameters); 
        //    }
        //}
        //return;


        Console.WriteLine("=== 文本向量生成示例 ===");

        // 模型和分词器路径
        //var modelPath = @"C:\GPT\ONNX\jina-embeddings-v3\onnx\model.onnx";
        //var tokenizerPath = @"C:\GPT\ONNX\jina-embeddings-v3\tokenizer.json";

        // 检查文件是否存在
        if (!File.Exists(modelPath) || !File.Exists(tokenizerPath))
        {
            Console.WriteLine("❌ 模型文件或分词器文件不存在，请检查路径");
            return;
        }

        try
        {
            while (true)
            {
                Console.WriteLine("请选择简历：");
                Console.WriteLine("1、cv1.md   2、cv2.md    3、cv3.md");
                var cvPath = "";
                var cvNo = Console.ReadLine();
                switch (cvNo)
                {
                    case "1":
                        cvPath = "cv1.md";
                        break;
                    case "2":
                        cvPath = "cv2.md";
                        break;
                    case "3":
                        cvPath = "cv3.md";
                        break;
                    default:
                        return;
                }
                var arr = File.ReadLines(cvPath).ToArray()[5..];
                var search = string.Join("", arr);
                Console.WriteLine($"简历: {search}");

                var sw = Stopwatch.StartNew();
                var searchVector = vectorGenerator.GenerateVector(search);
                Console.WriteLine("=======================Vector 搜索结果排序========================");
                var list = new List<Job>();
                foreach (var item in QueryVector(searchVector))
                {
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine($"职位: {item.Name}");
                    Console.WriteLine($"相似度: {item.Result}");
                    list.Add(jobs.SingleOrDefault(s => s.JobTitle == item.Name));
                }
                sw.Stop();
                Console.WriteLine($"========================Vector 搜索用时：{sw.ElapsedMilliseconds}毫秒==============================");

                sw = Stopwatch.StartNew();
                Console.WriteLine("=======================Reranker 搜索结果排序========================");
                Reranker(search, list);
                sw.Stop();
                Console.WriteLine($"==========================Reranker 搜索用时：{sw.ElapsedMilliseconds}毫秒============================");
            }
            vectorGenerator.Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ 错误: {ex.Message}");
        }
    }
    static void Reranker(string query, List<Job> jobs)
    {
        var modelPath = @"C:\GPT\ONNX\gte-multilingual-reranker-base-onnx-op14-opt-gpu\model.onnx";
        var tokenizerPath = @"C:\GPT\ONNX\gte-multilingual-reranker-base-onnx-op14-opt-gpu\tokenizer.json";
        var tokenizer = Tokenizers.HuggingFace.Tokenizer.Tokenizer.FromFile(tokenizerPath);

        using var session = new InferenceSession(modelPath);
        // query = query.Length >= 1024 ? query[..1023] : query;
        var newlist = new List<(string Title, float Score)>();
        foreach (var job in jobs)
        {
            //Console.WriteLine(job.JobTitle);
            var encodingResult = tokenizer.Encode(
                query,//.Replace(" ", ""),
                addSpecialTokens: true,
                input2: job.Description,
                includeTypeIds: true,
                includeAttentionMask: true
            );
            var enc = encodingResult.First();
            int seqLen = enc.Ids.Count;
            var inputIdsTensor = new DenseTensor<long>(enc.Ids.Select(i => (long)i).ToArray(), new[] { 1, seqLen });
            var typeIdsTensor = new DenseTensor<long>(enc.TypeIds.Select(i => (long)i).ToArray(), new[] { 1, seqLen });
            var attentionMaskTensor = new DenseTensor<long>(enc.AttentionMask.Select(i => (long)i).ToArray(), new[] { 1, seqLen });
            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input_ids",     inputIdsTensor),
                NamedOnnxValue.CreateFromTensor("attention_mask",attentionMaskTensor)
            };
            if (session.InputMetadata.ContainsKey("token_type_ids"))
            {
                inputs.Add(NamedOnnxValue.CreateFromTensor("token_type_ids", typeIdsTensor));
            }
            using var results = session.Run(inputs);
            var logitsArr = results.First(x => x.Name == "logits")
                                    .AsTensor<float>()
                                    .ToArray();
            float posLogit = logitsArr.Length == 1
                ? logitsArr[0]
                : logitsArr[1];

            float prob = 1f / (1f + MathF.Exp(-posLogit));

            newlist.Add((Title: job.JobTitle, Socre: prob));

        }

        foreach (var item in newlist.OrderByDescending(s => s.Score))
        {
            Console.WriteLine($"{item.Title}:{item.Score}");
        }

    }


    static IEnumerable<QueryResult> QueryVector(float[] vector)
    {
        var ds = new List<double>();
        foreach (var item in vector)
        {
            ds.Add((double)item);
        }
        using (IDbConnection db = new NpgsqlConnection(File.ReadAllText("C://GPT/just-agi-db.txt")))
        {
            string sqlQuery = $@"select id,name,1-(cast(@embedding as vector) <=> embedding) as result from public.imagevector1 order by 1-(cast(@embedding as vector) <=> embedding) desc ";
            return db.Query<QueryResult>(sqlQuery, new { embedding = ds });
        }
    }
    /// <summary>
    /// 计算余弦相似度
    /// </summary>
    private static float CosineSimilarity(float[] vector1, float[] vector2)
    {
        if (vector1.Length != vector2.Length)
            throw new ArgumentException("向量维度必须相同");

        float dotProduct = 0;
        float norm1 = 0;
        float norm2 = 0;

        for (int i = 0; i < vector1.Length; i++)
        {
            dotProduct += vector1[i] * vector2[i];
            norm1 += vector1[i] * vector1[i];
            norm2 += vector2[i] * vector2[i];
        }

        return dotProduct / (MathF.Sqrt(norm1) * MathF.Sqrt(norm2));
    }
}

/// <summary>
/// 基于 ONNX 的文本向量生成器
/// </summary>
public class TextVectorGenerator : IDisposable
{
    private readonly InferenceSession _session;
    private readonly Tokenizers.HuggingFace.Tokenizer.Tokenizer _tokenizer;
    private bool _disposed = false;

    public TextVectorGenerator(string modelPath, string tokenizerPath)
    {
        // 初始化 ONNX 推理会话
        var sessionOptions = new SessionOptions();

        // 如果有 GPU，可以启用 CUDA 提供程序
        // sessionOptions.AppendExecutionProvider_CUDA(0);

        _session = new InferenceSession(modelPath, sessionOptions);

        // 加载 HuggingFace 分词器
        _tokenizer = Tokenizers.HuggingFace.Tokenizer.Tokenizer.FromFile(tokenizerPath);

        // 输出模型的输入要求信息
        Console.WriteLine("模型输入要求:");
        foreach (var input in _session.InputMetadata)
        {
            Console.WriteLine($"- {input.Key}: {input.Value.ElementType} {string.Join(",", input.Value.Dimensions)}");
        }
    }

    /// <summary>
    /// 将文本转换为向量
    /// </summary>
    /// <param name="text">输入文本</param>
    /// <param name="taskType">任务类型：retrieval.query, retrieval.passage, text-matching, classification, separation, clustering</param>
    /// <returns>文本向量</returns>
    public float[] GenerateVector(string text, string taskType = "retrieval.passage")
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentException("输入文本不能为空", nameof(text));

        try
        {
            // 使用分词器对文本进行编码
            var encodingResult = _tokenizer.Encode(
                text,
                addSpecialTokens: true,
                includeAttentionMask: true
            );

            // 获取第一个编码对象
            var enc = encodingResult.First();
            var tokens = enc.Ids;
            var attentionMask = enc.AttentionMask;

            // 转换为张量
            var inputIds = new DenseTensor<long>(
                tokens.Select(t => (long)t).ToArray(),
                new[] { 1, tokens.Count }
            );

            var attentionMaskTensor = new DenseTensor<long>(
                attentionMask.Select(m => (long)m).ToArray(),
                new[] { 1, attentionMask.Count }
            );

            // 准备输入
            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input_ids", inputIds),
                NamedOnnxValue.CreateFromTensor("attention_mask", attentionMaskTensor)
            };

            // 检查并添加 task_id 输入（Jina Embeddings v3 特殊要求）
            if (_session.InputMetadata.ContainsKey("task_id"))
            {
                var taskId = GetTaskId(taskType);
                var taskIdTensor = new DenseTensor<long>(new[] { taskId }, new[] { 1 });
                inputs.Add(NamedOnnxValue.CreateFromTensor("task_id", taskIdTensor));
            }

            // 运行推理
            using var results = _session.Run(inputs);

            // 获取输出张量（通常是 last_hidden_state）
            var outputTensor = results.FirstOrDefault()?.AsTensor<float>();
            if (outputTensor == null)
                throw new InvalidOperationException("无法从模型获取输出");

            // 执行平均池化以获得句子向量
            var attentionMaskInt = attentionMask.Select(m => Convert.ToInt32(m)).ToArray();
            return PerformMeanPooling(outputTensor, attentionMaskInt);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"生成向量时出错: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// 根据任务类型获取对应的 task_id
    /// </summary>
    /// <param name="taskType">任务类型</param>
    /// <returns>task_id</returns>
    private long GetTaskId(string taskType)
    {
        return taskType.ToLower() switch
        {
            "retrieval.query" => 0,      // 检索查询
            "retrieval.passage" => 1,    // 检索文档 (默认)
            "text-matching" => 2,        // 文本匹配
            "classification" => 3,       // 文本分类
            "separation" => 4,           // 文本分离
            "clustering" => 5,           // 文本聚类
            _ => 1                       // 默认为检索文档
        };
    }

    /// <summary>
    /// 批量生成向量
    /// </summary>
    /// <param name="texts">文本列表</param>
    /// <param name="taskType">任务类型</param>
    /// <returns>向量列表</returns>
    public List<float[]> GenerateVectors(IEnumerable<string> texts, string taskType = "retrieval.passage")
    {
        var results = new List<float[]>();

        foreach (var text in texts)
        {
            results.Add(GenerateVector(text, taskType));
        }

        return results;
    }

    /// <summary>
    /// 异步生成向量
    /// </summary>
    /// <param name="text">输入文本</param>
    /// <param name="taskType">任务类型</param>
    /// <returns>文本向量</returns>
    public async Task<float[]> GenerateVectorAsync(string text, string taskType = "retrieval.passage")
    {
        return await Task.Run(() => GenerateVector(text, taskType));
    }

    /// <summary>
    /// 执行平均池化
    /// </summary>
    private float[] PerformMeanPooling(Tensor<float> hiddenStates, int[] attentionMask)
    {
        var sequenceLength = hiddenStates.Dimensions[1];
        var hiddenSize = hiddenStates.Dimensions[2];

        var pooledVector = new float[hiddenSize];
        var validTokenCount = 0;

        // 对所有有效 token 的隐藏状态进行平均
        for (int tokenIdx = 0; tokenIdx < sequenceLength; tokenIdx++)
        {
            if (attentionMask[tokenIdx] == 1) // 有效 token
            {
                for (int hiddenIdx = 0; hiddenIdx < hiddenSize; hiddenIdx++)
                {
                    pooledVector[hiddenIdx] += hiddenStates[0, tokenIdx, hiddenIdx];
                }
                validTokenCount++;
            }
        }

        // 计算平均值
        if (validTokenCount > 0)
        {
            for (int i = 0; i < hiddenSize; i++)
            {
                pooledVector[i] /= validTokenCount;
            }
        }

        return pooledVector;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _session?.Dispose();
            _disposed = true;
        }
    }
}

/// <summary>
/// 向量操作工具类
/// </summary>
public static class VectorUtils
{
    /// <summary>
    /// 计算两个向量的余弦相似度
    /// </summary>
    /// <param name="vector1">向量1</param>
    /// <param name="vector2">向量2</param>
    /// <returns>相似度值 (-1 到 1)</returns>
    public static float CosineSimilarity(ReadOnlySpan<float> vector1, ReadOnlySpan<float> vector2)
    {
        if (vector1.Length != vector2.Length)
            throw new ArgumentException("向量维度必须相同");

        float dotProduct = 0;
        float norm1 = 0;
        float norm2 = 0;

        for (int i = 0; i < vector1.Length; i++)
        {
            dotProduct += vector1[i] * vector2[i];
            norm1 += vector1[i] * vector1[i];
            norm2 += vector2[i] * vector2[i];
        }

        var denominator = MathF.Sqrt(norm1) * MathF.Sqrt(norm2);
        return denominator == 0 ? 0 : dotProduct / denominator;
    }

    /// <summary>
    /// 计算欧几里得距离
    /// </summary>
    /// <param name="vector1">向量1</param>
    /// <param name="vector2">向量2</param>
    /// <returns>欧几里得距离</returns>
    public static float EuclideanDistance(ReadOnlySpan<float> vector1, ReadOnlySpan<float> vector2)
    {
        if (vector1.Length != vector2.Length)
            throw new ArgumentException("向量维度必须相同");

        float sum = 0;
        for (int i = 0; i < vector1.Length; i++)
        {
            var diff = vector1[i] - vector2[i];
            sum += diff * diff;
        }

        return MathF.Sqrt(sum);
    }

    /// <summary>
    /// 向量归一化
    /// </summary>
    /// <param name="vector">输入向量</param>
    /// <returns>归一化后的向量</returns>
    public static float[] Normalize(ReadOnlySpan<float> vector)
    {
        float norm = 0;
        for (int i = 0; i < vector.Length; i++)
        {
            norm += vector[i] * vector[i];
        }

        norm = MathF.Sqrt(norm);
        if (norm == 0) return vector.ToArray();

        var normalized = new float[vector.Length];
        for (int i = 0; i < vector.Length; i++)
        {
            normalized[i] = vector[i] / norm;
        }

        return normalized;
    }
}