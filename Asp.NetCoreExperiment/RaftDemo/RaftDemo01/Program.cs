using Raft;
using Raft.Transport;
using System;

namespace RaftDemo01
{
    class Program
    {
        static IWarningLog log = null;
        static void Main(string[] args)
        {

            log = new Logger();

            var config = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + @"\a.txt");
            TcpRaftNode rn1 = TcpRaftNode.GetFromConfig(1, config,
                           System.IO.Directory.GetCurrentDirectory() + @"\DBreeze\Node1", 4250, log,
                           (entityName, index, data) =>
                           {
                               Console.WriteLine($"Committed {entityName}/{index}");
                               Console.WriteLine("4250数据：" + System.Text.Encoding.UTF8.GetString(data));
                               return true;
                           });

            TcpRaftNode rn2 = TcpRaftNode.GetFromConfig(1, config,
                           System.IO.Directory.GetCurrentDirectory() + @"\DBreeze\Node2", 4251, log,
                           (entityName, index, data) =>
                           {
                               Console.WriteLine($"Committed {entityName}/{index}");
                               Console.WriteLine("4251数据：" + System.Text.Encoding.UTF8.GetString(data));
                               return true;
                           });

            TcpRaftNode rn3 = TcpRaftNode.GetFromConfig(1, config,
                           System.IO.Directory.GetCurrentDirectory() + @"\DBreeze\Node3", 4252, log,
                           (entityName, index, data) =>
                           {
                               Console.WriteLine($"Committed {entityName}/{index}");
                               Console.WriteLine("4252数据：" + System.Text.Encoding.UTF8.GetString(data));
                               return true;
                           });

            rn1.Start();
            rn2.Start();
            rn3.Start();


            while (true)
            {
                Console.WriteLine("选择要发送的：1、4250     2、4251     3、4252");
                switch (Console.ReadLine())
                {
                    case "1":
                        rn1.AddLogEntry(System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString()));
                        break;
                    case "2":
                        rn2.AddLogEntry(System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString()));
                        break;
                    case "3":
                        rn3.AddLogEntry(System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString()));
                        break;
                }
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
