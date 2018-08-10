using Proto.Remote;
using Proto.Serialization.Wire;
using ServiceEntity;
using System;

namespace ServiceB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "客户端";
            Console.WriteLine("回车开始");       

            var wire = new WireSerializer(new[] { typeof(HelloRequest), typeof(HelloResponse) });
            Serialization.RegisterSerializer(wire, true);
       
            Remote.Start("127.0.0.1", 12001);
            var pid = Remote.SpawnNamedAsync("127.0.0.1:12000", "remote", "hello", TimeSpan.FromSeconds(5)).Result.Pid;
            while (true)
            {
                Console.WriteLine("请输入一个数字：");
                var num = int.Parse(Console.ReadLine());
                var res = pid.RequestAsync<HelloResponse>(new HelloRequest { Message = "请求：我是客户端", Amount= num }).Result;
                Console.WriteLine(res.Message);
              
            }
        }
    }
}
