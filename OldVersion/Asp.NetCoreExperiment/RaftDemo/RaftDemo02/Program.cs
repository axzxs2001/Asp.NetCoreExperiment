using Raft;
using Raft.Transport;
using System;
using System.Configuration;
namespace RaftDemo02
{
    class Program
    {
        static IWarningLog log = null;
        static void Main(string[] args)
        {
            var port = int.Parse(ConfigurationManager.AppSettings["port"]);
            var name = ConfigurationManager.AppSettings["name"];

            Console.Title = name + "port:" + port;
            log = new Logger();
            var config = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + @"\config.txt");
            var node = TcpRaftNode.GetFromConfig(1, config,
                           System.IO.Directory.GetCurrentDirectory() + $@"\DBreeze\{name}", port, log,
                           (entityName, index, data) =>
                           {
                               Console.WriteLine($"{entityName}/{index} { System.Text.Encoding.UTF8.GetString(data)}");
                               return true;
                           });
           
            node.Start();
                  
            while (true)
            {
                Console.WriteLine("输入发送的内容并回车：");
                node.AddLogEntry(System.Text.Encoding.UTF8.GetBytes(Console.ReadLine()));
            }

        }
        public class Logger : IWarningLog
        {
            public void Log(WarningLogEntry logEntry)
            {
                Console.WriteLine(logEntry.ToString());
            }
        }
    }
}
