using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Net.Http;
using System.Threading.Tasks;

BenchmarkRunner.Run<TestInvock>();

[MemoryDiagnoser]
public class TestInvock
{
    readonly HttpClient _invockClient;
    readonly HttpClient _sidecarClient;
    public TestInvock()
    {
        _invockClient = new HttpClient();
        _sidecarClient = new HttpClient();
    }

    [Benchmark]
    public async Task<string> Invoke()
    {
        var content = await _invockClient.GetStringAsync("http://localhost:5000/test");
        return content;
    }

    [Benchmark]
    public async Task<string> SidecarInvoke()
    {
        var content = await _sidecarClient.GetStringAsync("http://localhost:3500/v1.0/invoke/app1-1/method/test");
        return content;
    }
  
    [Benchmark]
    public async Task<string> LoadbalancingInvoke()
    {
        var content = await _sidecarClient.GetStringAsync("http://localhost:3500/v1.0/invoke/app1/method/test");
        return content;
    }
}