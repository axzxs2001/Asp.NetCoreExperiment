
var arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

//方法二
for (var i = 0; i < arr.Length; i++)
{
    arr[i] += 1;
}
Console .WriteLine(string.Join(',',arr));
//方法三
foreach (ref var i in arr.AsSpan())
{
    i++;
}
Console.WriteLine(string.Join(',', arr));
//using BenchmarkDotNet.Attributes;
//using BenchmarkDotNet.Running;

//BenchmarkRunner.Run<TestSpan>();

//public class TestSpan
//{

//    [Benchmark]
//    public void Demo1()
//    {
//        var arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
//        foreach (ref var i in arr.AsSpan())
//        {
//            i++;
//        }
//    }

//    [Benchmark]
//    public void Demo2()
//    {
//        var arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
//        for (var i = 0; i < arr.Length; i++)
//        {
//            arr[i] += 1;
//        }
//    }
//}