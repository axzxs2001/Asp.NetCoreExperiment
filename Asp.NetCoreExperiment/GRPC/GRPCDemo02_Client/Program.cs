using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Linq;

namespace GRPCDemo02_Client
{
    class Program
    {
        static void Main(string[] args)
        {

            Grpc.Tools.ProtoCompile c = new Grpc.Tools.ProtoCompile();
            c.Protobuf = Utils.MakeSimpleItems(@"C:\MyFile\grpc\Protos\goods.proto"); 
            c.Generator = "csharp";
            c.OutputDir = "c:/myfile/grpc/";
            c.GrpcPluginExe= "grpcgen";
           
           // c.GrpcOutputDir = "c:/myfile/grpc/a.cs";
            c.ProtoPath = new string[] { @"C:\MyFile\grpc" };
            c.Execute();
            //Grpc.Tools.ProtoToolsPlatform p = new Grpc.Tools.ProtoToolsPlatform();
            //Grpc.Tools.ProtoCompilerOutputs po = new Grpc.Tools.ProtoCompilerOutputs();
            //Grpc.Tools.ProtoReadDependencies pd = new Grpc.Tools.ProtoReadDependencies();

            Console.WriteLine("Hello World!");
        }
    }

    static class Utils
    {
        // Build an item with a name from args[0] and metadata key-value pairs
        // from the rest of args, interleaved.
        // This does not do any checking, and expects an odd number of args.
        public static ITaskItem MakeItem(params string[] args)
        {
            var item = new TaskItem(args[0]);
            for (int i = 1; i < args.Length; i += 2)
            {
                item.SetMetadata(args[i], args[i + 1]);
            }
            return item;
        }

        // Return an array of items from given itemspecs.
        public static ITaskItem[] MakeSimpleItems(params string[] specs)
        {
            return specs.Select(s => new TaskItem(s)).ToArray();
        }
    };
}
