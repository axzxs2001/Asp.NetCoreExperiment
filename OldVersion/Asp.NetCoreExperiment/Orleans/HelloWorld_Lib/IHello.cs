using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Linq;
using Orleans;
using Orleans.Runtime;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld_Lib
{

    public interface IHelloWorldGrain : IGrainWithGuidKey
    {
        Task Method1();
    }

    public class HelloWorldGrain : Grain, IHelloWorldGrain
    {
        readonly IAAA _aAA;
        public HelloWorldGrain(IAAA aAA)
        {
            _aAA = aAA;
        }

        public Task Method1()
        {
            _aAA.F();
            Console.WriteLine("这里是Grain的一个类，IHelloWorld.Method1");
            return Task.CompletedTask;
        }

    }


    public interface IAAA : IGrainExtension
    {
        Task F();
    }
    public class AAA : IAAA
    {
        readonly IConfiguration _configuration;
        public AAA(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task F()
        {
            Console.WriteLine($"这是AAA里的F方法，读取配置文件:{_configuration.GetSection("name").Value}");
            Console.WriteLine($"这是AAA里的F方法，读取配置文件:{_configuration.GetSection("a").GetSection("a2").GetSection("a21").Value}");
            Console.WriteLine($"这是AAA里的F方法，读取配置文件:{_configuration.GetSection("a:a2:a22").Value}");
            return Task.CompletedTask;
        }
    }
}