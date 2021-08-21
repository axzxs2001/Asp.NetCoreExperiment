using RulesEngine;
using RulesEngine.Models;
using RulesEngine.Extensions;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

var path = @"C:\MyFile\Source\Repos\Asp.NetCoreExperiment\Asp.NetCoreExperiment\GoReplay\GoReplayDemo01\request_0.gor";
await ImpurityRemoval(path);


static async Task ImpurityRemoval(string path)
{
    string? line;
    using var file = new StreamReader(path, Encoding.UTF8);

    using var writeFile = new StreamWriter(@$"{path.Replace(Path.GetFileName(path), Path.GetFileNameWithoutExtension(path) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(path))}", true, Encoding.UTF8);

    var split = "🐵🙈🙉";
    var request = new StringBuilder();
    while ((line = await file.ReadLineAsync()) != null)
    {
        if (line != split)
        {
            request.Append(line + "\n");
        }
        else
        {
            request.Append(line + "\n");
            var list = GetJson(request.ToString());
            var exc = "input1.Amount >= 900.00";
            foreach (var item in list)
            {
                var pay = System.Text.Json.JsonSerializer.Deserialize<Pay>(item, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (await Filter(pay, exc))
                {
                    await writeFile.WriteAsync(request.ToString());
                }
            }
            request.Clear();
        }
    }
}

static List<string> GetJson(string jsonString)
{
    var pattern = @"\{(.|\s)*\}";
    var list = new List<string>();
    var matches = Regex.Matches(jsonString, pattern, RegexOptions.IgnoreCase);
    foreach (Match m in matches)
    {
        list.Add(m.Value);
    }
    return list;
}

static async Task<bool> Filter(Pay? pay, string expression)
{
    var workRules = new WorkflowRules();
    workRules.WorkflowName = "过滤";
    workRules.Rules = new List<Rule>
            {
                new Rule
                {
                    RuleName="过滤",
                    SuccessEvent= "10",
                    RuleExpressionType= RuleExpressionType.LambdaExpression,
                    Expression= expression,
                    Enabled=true,
                }
            };


    var rulesEngine = new RulesEngine.RulesEngine(new WorkflowRules[] { workRules });
    List<RuleResultTree> resultList = await rulesEngine.ExecuteAllRulesAsync("过滤", pay);
    var result = false;
    resultList.OnSuccess((eventName) =>
    {
        result = true;
    });
    return result;
}



public class Pay
{
    public string Code { get; set; }
    public decimal Amount { get; set; }
    public int Status { get; set; }
}