using RulesEngine;
using RulesEngine.Models;
using RulesEngine.Extensions;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Dynamic;

var path = @"C:\MyFile\Source\Repos\Asp.NetCoreExperiment\Asp.NetCoreExperiment\GoReplay\GoReplayDemo01\request_0.gor";
var expression = "input1.amount >= 900.00";
await ImpurityRemoval(path, expression);

/// <summary>
/// 除杂方法，会重新生成一个带有日期时间的新.gor文件
/// </summary>
static async Task ImpurityRemoval(string path, string expression)
{
    using var readFile = new StreamReader(path, Encoding.UTF8);
    using var writeFile = new StreamWriter(@$"{path.Replace(Path.GetFileName(path), Path.GetFileNameWithoutExtension(path) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(path))}", true, Encoding.UTF8);
    var split = "🐵🙈🙉";
    string? line;
    var request = new StringBuilder();
    while ((line = await readFile.ReadLineAsync()) != null)
    {
        if (line != split)
        {
            request.Append(line + "\n");
        }
        else
        {
            request.Append(line + "\n");
            var list = GetJson(request.ToString());

            foreach (var item in list)
            {
                var converter = new ExpandoObjectConverter();
                var entity = JsonConvert.DeserializeObject<ExpandoObject>(item, converter);
                if (await Filter(entity, expression))
                {
                    await writeFile.WriteAsync(request.ToString());
                }
            }
            request.Clear();
        }
    }
}
/// <summary>
/// 获取json，这里没有完全测试
/// </summary>
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
/// <summary>
/// 用规则引擎匹配过滤规则
/// </summary>
static async Task<bool> Filter(dynamic? entity, string expression)
{
    var workRules = new WorkflowRules();
    workRules.WorkflowName = "ImpurityRemoval";
    workRules.Rules = new List<Rule>
    {
        new Rule
        {
            RuleName="ImpurityRemoval01",
            SuccessEvent= "10",
            RuleExpressionType= RuleExpressionType.LambdaExpression,
            Expression= expression,          
        }
    };
    var rulesEngine = new RulesEngine.RulesEngine(new WorkflowRules[] { workRules });
    List<RuleResultTree> resultList = await rulesEngine.ExecuteAllRulesAsync("ImpurityRemoval", entity);
    var result = false;
    resultList.OnSuccess((eventName) =>
    {
        result = true;
    });
    return result;
}