using Messages;
using Proto.Remote;
using ProtoRemote01;
using System;
using ProtosReflection = Messages.ProtosReflection;
namespace ProtoRemote02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "客户端";
            Console.WriteLine("回车开始");
            Console.ReadLine();
            Serialization.RegisterFileDescriptor(ProtosReflection.Descriptor);
            Remote.Start("127.0.0.1", 12001);           
      
            var pid = Remote.SpawnNamedAsync("127.0.0.1:12000","remote","hello", TimeSpan.FromSeconds(5)).Result.Pid;
            var res = pid.RequestAsync<HelloResponse>(new HelloRequest { }).Result;
            Console.WriteLine(res.Message);
            Console.ReadLine();
        }
    }
}
