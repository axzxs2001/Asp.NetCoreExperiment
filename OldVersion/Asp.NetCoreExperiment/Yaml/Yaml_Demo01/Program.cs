using System;
using YamlDotNet.Serialization;
namespace Yaml_Demo01
{
    class Program
    {
        static void Main(string[] args)
        {
            var serializer = new Serializer();
            var yaml = serializer.Serialize(new ABC { ID = 1, Name = "abc", BCD = new BCD { Time = DateTime.Now } });
            Console.WriteLine(yaml);

            var deserializer = new Deserializer();
            var abc = deserializer.Deserialize<ABC>(yaml);
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(abc));
            Console.ReadLine();
        }
    }
    class ABC
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public BCD BCD { get; set; }
    }
    class BCD
    {
        public DateTime Time { get; set; }
    }
}
