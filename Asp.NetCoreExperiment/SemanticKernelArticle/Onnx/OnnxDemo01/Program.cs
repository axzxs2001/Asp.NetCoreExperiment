using Messages.Tokenizer;
using Messages.Trainers;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.OnnxRuntimeGenAI;
using System;
using System.Linq;
using Tokenizers.DotNet;
//Reranker();
Reranker2();


static void Reranker2()
{
    var modelPath = @"C:\GPT\ONNX\BGERerankerLarge\onnx\model.onnx";
    var tokenizerPath = @"C:\GPT\ONNX\BGERerankerLarge\tokenizer.json";

    var tokenizer = Tokenizers.HuggingFace.Tokenizer.Tokenizer.FromFile(tokenizerPath);
    using var session = new InferenceSession(modelPath);

    //string query = "苹果和安卓买那个好";
    //string[] docs = {
    //        "苹果手机的用户体验要明显好的多",
    //        "苹果的设备一般都很贵",
    //        "苹果很甜很脆",
    //    };
    string query = "appleとアンドロイド、どちらを買うのが良いか？";
    string[] docs = {
    "appleのスマホのユーザー体験は明らかに良い。",
    "appleのデバイスは一般的に高価である。",
    "appleは甘くてシャキシャキしている。",
};

    foreach (var doc in docs)
    {
        var encodingResult = tokenizer.Encode(
            query,
            add_special_tokens: true,
            input2: doc,
            include_type_ids: true,
            include_attention_mask: true
        );
        var enc = encodingResult.Encodings[0];
        int seqLen = enc.Ids.Count;
        var inputIdsTensor = new DenseTensor<long>(enc.Ids.Select(i => (long)i).ToArray(), new[] { 1, seqLen });
        var attentionMaskTensor = new DenseTensor<long>(enc.AttentionMask.Select(i => (long)i).ToArray(), new[] { 1, seqLen });
        var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input_ids",     inputIdsTensor),
                NamedOnnxValue.CreateFromTensor("attention_mask",attentionMaskTensor)
            };

        using var results = session.Run(inputs);
        var logitsArr = results.First(x => x.Name == "logits")
                                .AsTensor<float>()
                                .ToArray();
        float posLogit = logitsArr.Length == 1
            ? logitsArr[0]
            : logitsArr[1];

        float prob = 1f / (1f + MathF.Exp(-posLogit));

        Console.WriteLine($"Doc: \"{doc}\"");
        Console.WriteLine($"  正类 logit:    {posLogit:F4}");
        Console.WriteLine($"  相关度概率:    {prob:F4}");
        Console.WriteLine(new string('-', 40));
    }
}



static void Reranker1()
{

    var modelPath = "C:\\GPT\\ONNX\\Qwen3Reranker4B\\model.onnx";

    var tokenizerPath = "C:\\GPT\\ONNX\\Qwen3Reranker4B\\tokenizer.json";

    var tokenizer = Tokenizers.HuggingFace.Tokenizer.Tokenizer.FromFile(tokenizerPath);
    using var session = new InferenceSession(modelPath);


    string query = "苹果手机多少钱？";
    string[] docs = {
            "苹果手机的用户体验要明显好的多",
            "苹果的设备一般都很贵",
            "富士苹果很甜很脆",
        };


    foreach (var doc in docs)
    {

        // 使用支持对输入的 Encode 重载，添加 special tokens，返回 type_ids 与 attention_mask
        // Python 对应 encode_plus(query, doc, add_special_tokens=True, max_length=512, ...) ([stackoverflow.com](https://stackoverflow.com/questions/61708486/whats-difference-between-tokenizer-encode-and-tokenizer-encode-plus-in-hugging?utm_source=chatgpt.com))
        var encodingResult = tokenizer.Encode(
            query,                   // 第一个序列
            add_special_tokens: true,  // 添加 [CLS], [SEP] 等特殊 token
            input2: doc,                     // 第二个序列
            include_type_ids: true,    // 返回 token_type_ids
            include_attention_mask: true
        );

        // Encode 的结果包含一个 Encoding 对象，存储在 Encodings[0]
        var enc = encodingResult.Encodings[0];

        // 从 enc 中提取各项
        var inputIds = enc.Ids;             // List<int>
        var typeIds = enc.TypeIds;         // List<int>
        var attentionMask = enc.AttentionMask;   // List<int>




        // 将 List<int> 转为 DenseTensor<long>，形状为 [1, seqLen]
        int seqLen = inputIds.Count;
        var inputIdsTensor = new DenseTensor<long>(inputIds.Select(i => (long)i).ToArray(), new[] { 1, seqLen });
        var typeIdsTensor = new DenseTensor<long>(typeIds.Select(i => (long)i).ToArray(), new[] { 1, seqLen });
        var attentionMaskTensor = new DenseTensor<long>(attentionMask.Select(i => (long)i).ToArray(), new[] { 1, seqLen });

        // 创建模型输入
        var inputs = new[]
        {
            NamedOnnxValue.CreateFromTensor("input_ids",      inputIdsTensor),
            //NamedOnnxValue.CreateFromTensor("token_type_ids",  typeIdsTensor),
            NamedOnnxValue.CreateFromTensor("attention_mask",  attentionMaskTensor)
        };

        // 运行推理
        using var results = session.Run(inputs);

        // 假设输出名为 "logits"，并为 [1, 1] 张量
        var logitsTensor = results.First(x => x.Name == "logits").AsEnumerable<float>().ToArray();
        float score = logitsTensor[0];

        Console.WriteLine($"{doc},文档相关性得分: {score:F4}");
    }
}

static void Reranker()
{

    var modelPath = @"C:\GPT\ONNX\BGERerankerLarge\onnx\model.onnx";
    var tokenizerPath = @"C:\GPT\ONNX\BGERerankerLarge\tokenizer.json";

    // 加载 tokenizer 和模型
    var tokenizer = new Tokenizers.DotNet.Tokenizer(tokenizerPath);
    using var session = new InferenceSession(modelPath);


    // 示例 query 和文档列表
    string query = "苹果和安卓相比怎么样";
    string[] docs = {
            "苹果手机的用户体验要明显好的多",
            "苹果的设备一般都很贵",
            "红苹果很甜很脆",
        };

    // 编码输入并推理
    var scores = docs.Select(doc =>
    {
        // Combine query + doc
        var text = $"{query}[SEP]{doc}";
        // var text = $"{query}[SEP]";
        var ids = tokenizer.Encode(text);

        // 构建输入张量
        var seqLen = ids.Length;
        var inputIds = new DenseTensor<long>(ids.Select(i => (long)i).ToArray(), new[] { 1, seqLen });
        var attention = new DenseTensor<long>(Enumerable.Repeat(1L, seqLen).ToArray(), new[] { 1, seqLen });

        var inputs = new[] {
                NamedOnnxValue.CreateFromTensor("input_ids", inputIds),
                NamedOnnxValue.CreateFromTensor("attention_mask", attention)
            };

        // 推理
        using var results = session.Run(inputs);
        var logits = results.First().AsTensor<float>();
        return logits[0];
    }).ToArray();

    // 排序并输出
    var ranked = docs.Zip(scores, (doc, score) => new { doc, score })
                     .OrderByDescending(x => x.score);

    Console.WriteLine("=== Reranking Results ===");
    foreach (var item in ranked)
        Console.WriteLine($"{item.score:F4}\t{item.doc}");



}


static void Reranker3()
{
    // 模型和分词器路径
    var modelPath = @"C:\GPT\ONNX\Qwen3Reranker4B\model.onnx";
    var tokenizerPath = @"C:\GPT\ONNX\Qwen3Reranker4B\tokenizer.json";

    // 加载 tokenizer 与模型
    var tokenizer = new Tokenizers.DotNet.Tokenizer(tokenizerPath);
    using var session = new InferenceSession(modelPath);


    string query = "想买个手机";
    string[] docs = {
                    "苹果手机的用户体验要明显好的多",
                    "苹果的设备一般都很贵",
                    "国光苹果很甜很脆",
                };

    var ranked = RerankDocuments(session, tokenizer, query, docs);

    Console.WriteLine("=== Reranking Results ===");
    Console.WriteLine("RawLogit\tProbability\tDocument");
    foreach (var item in ranked)
    {
        Console.WriteLine($"{item.Score,8:F4}\t{item.Probability,10:F4}\t{item.Doc}");
    }

    Console.WriteLine();
    System.Threading.Thread.Sleep(2000);

}

/// <summary>
/// 对一组文档根据 query 进行重排序，返回原始分数和概率
/// </summary>
static List<(string Doc, float Score, float Probability)> RerankDocuments(
    InferenceSession session,
    Tokenizers.DotNet.Tokenizer tokenizer,
    string query,
    string[] docs)
{
    var results = new List<(string, float, float)>();

    foreach (var doc in docs)
    {
        // 1. 拼接输入：query + [SEP] + doc
        var text = $"{query}[SEP]{doc}";
        var ids = tokenizer.Encode(text);

        // 2. 构建 ONNX 输入张量
        int seqLen = ids.Length;
        var inputIds = new DenseTensor<long>(ids.Select(i => (long)i).ToArray(), new[] { 1, seqLen });
        var attMask = new DenseTensor<long>(Enumerable.Repeat(1L, seqLen).ToArray(), new[] { 1, seqLen });

        var inputs = new List<NamedOnnxValue> {
                    NamedOnnxValue.CreateFromTensor("input_ids", inputIds),
                    NamedOnnxValue.CreateFromTensor("attention_mask", attMask)
                };

        // 3. 推理
        using var output = session.Run(inputs);
        var logitsTensor = output.First().AsTensor<float>();
        var raw = logitsTensor.ToArray();

        // 4. 根据输出长度计算分数与概率
        float score, prob;
        if (raw.Length == 1)
        {
            // 单输出：用 sigmoid
            score = raw[0];
            prob = Sigmoid(score);
        }
        else if (raw.Length == 2)
        {
            // 二分类：Softmax 后取正类（index=1）
            score = raw[1];
            var exp0 = MathF.Exp(raw[0]);
            var exp1 = MathF.Exp(raw[1]);
            prob = exp1 / (exp0 + exp1);
        }
        else
        {
            throw new InvalidOperationException($"Unexpected logit length: {raw.Length}");
        }

        results.Add((doc, score, prob));
    }

    // 按原始 score 排序（和按 prob 排序效果等价）
    return results
        .OrderByDescending(x => x.Item2)
        .ToList();
}

/// <summary>
/// Sigmoid 函数：把任意实数映射到 (0,1)
/// </summary>
static float Sigmoid(float x)
    => 1f / (1f + MathF.Exp(-x));



static void Phi()
{
    string modelDir = @"C:\Users\axzxs\phi\cpu_and_mobile\cpu-int4-rtn-block-32-acc-level-4";
    using var model = new Model(modelDir);
    using var tokenizer = new Microsoft.ML.OnnxRuntimeGenAI.Tokenizer(model);

    Console.WriteLine("模型加载完成，输入 prompt：");

    while (true)
    {
        Console.Write("\n> ");
        var prompt = Console.ReadLine();
        if (string.IsNullOrEmpty(prompt)) break;

        var full = $"<|user|>{prompt}<|end|><|assistant|>";
        var sequences = tokenizer.Encode(full);

        using var genParams = new GeneratorParams(model);
        genParams.SetSearchOption("max_length", 512);

        using var generator = new Generator(model, genParams);
        generator.AppendTokenSequences(sequences);  // 初始化输入！

        Console.Write("回复: ");
        using var tokenizerStream = tokenizer.CreateStream();

        while (!generator.IsDone())
        {
            generator.GenerateNextToken();
            var seq = generator.GetSequence(0);
            var last = seq[^1];
            Console.Write(tokenizerStream.Decode(last));
        }
        Console.WriteLine();
    }
}