using System;
using System.Linq;
using Tokenizers.DotNet;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.OnnxRuntimeGenAI;

var modelPath = "C:\\GPT\\ONNX\\Qwen3Reranker4B\\model.onnx";

var tokenizerPath = "C:\\GPT\\ONNX\\Qwen3Reranker4B\\tokenizer.json";

// 加载 tokenizer 和模型
var tokenizer = new Tokenizers.DotNet.Tokenizer(tokenizerPath);
using var session = new InferenceSession(modelPath);

while (true)
{
    // 示例 query 和文档列表
    string query = Console.ReadLine();// "人工智能的未来发展是什么？";
    string[] docs = {
            "人工智能将在医疗领域发挥更大作用",
            "气候变化对农业的影响",
            "AI 怎么改变教育系统",
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