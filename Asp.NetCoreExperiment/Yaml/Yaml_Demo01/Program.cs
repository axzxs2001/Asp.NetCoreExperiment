using System;
using YamlDotNet.Serialization;
namespace Yaml_Demo01
{
    class Program
    {
        static void Main(string[] args)
        {
            var serializer = new Serializer();
            var yaml = serializer.Serialize(new { id = 1, name = "abc",ttime=new { t=DateTime .Now} });
            Console.WriteLine(yaml);
        }
    }
}
