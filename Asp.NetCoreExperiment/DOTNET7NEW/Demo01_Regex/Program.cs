using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System.Diagnostics;
using System.Text.RegularExpressions;



//

//var summary = BenchmarkRunner.Run(typeof(Demo02));
//var demo02 = new Demo02();
//demo02.Test01();
//demo02.Test02();

var demo01 = new Demo01();
demo01.Setup();
demo01.Test01_C();
Console.WriteLine("----------------------------------");
demo01.Test01_D();


//[SimpleJob(RuntimeMoniker.Net60)]
//[SimpleJob(RuntimeMoniker.Net70)]
public class Demo01
{

    Dictionary<string, string> dic;
    [GlobalSetup]
    public void Setup()
    {
        dic = new Dictionary<string, string>()
        {
            {"15191845334151918453341519184533415191845334151918453341519184533415191845334151918453341519184533415191845334151918453341519184533415191845334","^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\\d{8}$"},
            {"http://www.google.comhttp://www.google.comhttp://www.google.comhttp://www.google.comhttp://www.google.com","[a-zA-z]+://[^\\s]* 或 ^http://([\\w-]+\\.)+[\\w-]+(/[\\w-./?%&=]*)?$"},
            {"axzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.comaxzxs2001@163.com","^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$"},
            {"021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822021-87888822","\\d{3}-\\d{8}|\\d{4}-\\d{7}" },

        };
        //dic = new Dictionary<string, string>();
     
        //for (int i = 10; i <= 20; i++)
        //{
        //    var r = $@"^(\w\d|\d\w){{{i}}}$";
        //    string input = new string('1', (i * 2) + 1);
        //    dic.Add(input, r);
        //}
    }

    [Benchmark]
    public void Test01_A()
    {
        //var sw = new Stopwatch();
        foreach (var item in dic)
        {
            var r = new Regex(item.Value, RegexOptions.NonBacktracking);
            //sw.Restart();
            r.IsMatch(item.Key);
            //sw.Stop();
            //Console.WriteLine($"{item.Key}: {sw.Elapsed.TotalMicroseconds}µs");
        }
    }
    [Benchmark]
    public void Test01_B()
    {
        // var sw = new Stopwatch();
        foreach (var item in dic)
        {
            var r = new Regex(item.Value);
            // sw.Restart();
            r.IsMatch(item.Key);
            //sw.Stop();
            // Console.WriteLine($"{item.Key}: {sw.Elapsed.TotalMicroseconds}µs");
        }
    }



    public void Test01_C()
    {
        var sw = new Stopwatch();
        foreach (var item in dic)
        {
            var r = new Regex(item.Value);
            sw.Restart();
            r.IsMatch(item.Key);
            sw.Stop();
            Console.WriteLine($"{sw.Elapsed.TotalMicroseconds}µs");
        }
    }

    public void Test01_D()
    {
        var sw = new Stopwatch();
        foreach (var item in dic)
        {
            var r = new Regex(item.Value, RegexOptions.NonBacktracking);
            sw.Restart();
            r.IsMatch(item.Key);
            sw.Stop();
            Console.WriteLine($"{sw.Elapsed.TotalMicroseconds}µs");
        }
    }
}

/*
var r1 = new Regex(@"^\w$");
var dt = DateTime.Now.ToString("yyyyy");
int t = 112_123_342;
decimal d = 12_243.333_323m;
Console.WriteLine(t);
Console.WriteLine(d);
*/

public class Demo02
{
    [Benchmark]
    public void Test01()
    {
        var sw = new Stopwatch();
        for (int i = 10; i <= 20; i++)
        {
            var r = new Regex($@"^(\w\d|\d\w){{{i}}}$");
            string input = new string('1', (i * 2) + 1);

            sw.Restart();
            r.IsMatch(input);
            sw.Stop();
            //Console.WriteLine($"{i}: {sw.Elapsed.TotalMilliseconds:N}ms");
        }
    }
    [Benchmark]

    public void Test02()
    {
        var sw = new Stopwatch();
        for (int i = 10; i <= 20; i++)
        {
            var r = new Regex($@"^(\w\d|\d\w){{{i}}}$", RegexOptions.NonBacktracking);
            string input = new string('1', (i * 2) + 1);

            sw.Restart();
            r.IsMatch(input);
            sw.Stop();
            // Console.WriteLine($"{i}: {sw.Elapsed.TotalMilliseconds:N}ms");
        }
    }

}