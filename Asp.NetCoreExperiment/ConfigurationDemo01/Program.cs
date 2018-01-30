using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System;
using System.Collections.Generic;

namespace ConfigurationDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new Dictionary<string, string>
            {
                ["LongDatePattern"] = "dddd, MMMM d, yyyy",
                ["LongTimePattern"] = "h:mm:ss tt",
                ["ShortDatePattern"] = "M/d/yyyy",
                ["ShortTimePattern"] = "h:mm tt"
            };

            var memorySource = new MemoryConfigurationSource();
            memorySource.InitialData = source;    
            var configuration = new ConfigurationBuilder()
                    .Add(memorySource)
                    .Build();

            var settings = new DateTimeFormatSettings(configuration);
            Console.WriteLine("{0,-16}: {1}", "LongDatePattern", settings.LongDatePattern);
            Console.WriteLine("{0,-16}: {1}", "LongTimePattern", settings.LongTimePattern);
            Console.WriteLine("{0,-16}: {1}", "ShortDatePattern", settings.ShortDatePattern);
            Console.WriteLine("{0,-16}: {1}", "ShortTimePattern", settings.ShortTimePattern);
        }
    }

    public class DateTimeFormatSettings
    {
        //其他成员
        public DateTimeFormatSettings(IConfiguration configuration)
        {
            this.LongDatePattern = configuration["LongDatePattern"];
            this.LongTimePattern = configuration["LongTimePattern"];
            this.ShortDatePattern = configuration["ShortDatePattern"];
            this.ShortTimePattern = configuration["ShortTimePattern"];
        }

        public string LongDatePattern { get; set; }
        public string LongTimePattern { get; set; }
        public string ShortDatePattern { get; set; }
        public string ShortTimePattern { get; set; }
    }
}
