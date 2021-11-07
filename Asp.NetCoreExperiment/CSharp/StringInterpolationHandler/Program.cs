using static System.Console;
using System.Runtime.CompilerServices;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

var name = "张三";
var total = 10000.0m;
var result = TestParameter("admin", $"本次向 {name} 转帐：{total} 元");
WriteLine(result);

result = TestParameter("viewer", $"本次向 {name} 转帐：{total} 元");
WriteLine(result);

static string TestParameter(string role, [InterpolatedStringHandlerArgument("role")] ParameterInterpolatedStringHandler handler)
{
    return handler.GetFormattedText();
}

//BenchmarkRunner.Run<Test>();

[InterpolatedStringHandler]
public ref struct ParameterInterpolatedStringHandler
{
    /// <summary>
    /// 构建字符串的stringbuilder
    /// </summary>
    StringBuilder builder;

    string _role;
    /// <summary>
    /// 两个长度是必需的
    /// </summary>
    /// <param name="literalLength"></param>
    /// <param name="formattedCount"></param>

    public ParameterInterpolatedStringHandler(int literalLength, int formattedCount, string role)
    {
        _role = role;
        builder = new StringBuilder(literalLength);
    }

    /// <summary>
    /// 添加非格式化部分，如果被 {}分隔，会调用多次
    /// </summary>
    /// <param name="s"></param>
    public void AppendLiteral(string s)
    {
        builder.Append(s);
    }
    /// <summary>
    /// 添加格式化部分，如果有多个{}，会调用多次
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    public void AppendFormatted<T>(T t)
    {
        if (_role.ToLower() == "admin")
        {
            builder.Append(t?.ToString());
        }
        else
        {
            builder.Append("******");
        }
    }
    internal string GetFormattedText() => builder.ToString();
}


#region part 1
public class Test
{
    [Benchmark]
    public void Test1()
    {
        var content = $"日期：{DateOnly.FromDateTime(DateTime.Now)}, 时间：{TimeOnly.FromDateTime(DateTime.Now)}";
        var result = WriteString(content);
        //WriteLine(result);
    }
    [Benchmark]
    public void Test2()
    {
        var result = WriteStringWithInterpolateHandler($"日期：{DateOnly.FromDateTime(DateTime.Now)}, 时间：{TimeOnly.FromDateTime(DateTime.Now)}");
        //WriteLine(result);
    }
    public string WriteString(string content)
    {
        return content;
    }

    public string WriteStringWithInterpolateHandler(TestInterpolatedStringHandler handler)
    {
        return handler.GetFormattedText();
    }
}
/// <summary>
/// string内插处理类型
/// </summary>
[InterpolatedStringHandler]
public ref struct TestInterpolatedStringHandler
{
    /// <summary>
    /// 构建字符串的stringbuilder
    /// </summary>
    StringBuilder builder;
    /// <summary>
    /// 两个长度是必需的
    /// </summary>
    /// <param name="literalLength"></param>
    /// <param name="formattedCount"></param>
    public TestInterpolatedStringHandler(int literalLength, int formattedCount)
    {
        builder = new StringBuilder(literalLength);
    }

    /// <summary>
    /// 添加非格式化部分，如果被 {}分隔，会调用多次
    /// </summary>
    /// <param name="s"></param>
    public void AppendLiteral(string s)
    {
        builder.Append(s);
    }
    /// <summary>
    /// 添加格式化部分，如果有多个{}，会调用多次
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    public void AppendFormatted<T>(T t)
    {
        builder.Append(t?.ToString());
    }
    internal string GetFormattedText() => builder.ToString();
}
#endregion