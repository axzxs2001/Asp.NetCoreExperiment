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
        public Task F()
        {           
            var jsonFile = $"{Directory.GetCurrentDirectory()}/appsettings.json";
            var content = File.ReadAllText(jsonFile, Encoding.Default);
            var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(content);
            Console.WriteLine($"这是AAA里的F方法，读取配置文件:{jsonObj.GetValue("name")}");
            return Task.CompletedTask;
        }
    }
}