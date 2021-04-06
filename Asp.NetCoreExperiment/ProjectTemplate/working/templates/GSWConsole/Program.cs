using System;
using Newtonsoft;
using Newtonsoft.Json;
using static System.Console;

namespace GSWCon
{
    class Program
    {
        static void Main(string[] args)
        {
            var template = new Template
            {
                Name = "GSWCon",
                Author = "桂素伟",
                Description = "第一个项目模板，控制台项目，引入了Newtonsoft库！",
                CreateTime = DateTime.Parse("2021-04-06 08:00:00")
            };
            WriteLine(JsonConvert.SerializeObject(template, Formatting.Indented));
        }
    }

    class Template
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
