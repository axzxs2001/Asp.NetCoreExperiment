using System;

namespace DynamicLoadDll
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = AssemblyLoader.LoadFromAssemblyPath("dll路径");
            Console.WriteLine("Hello World!");
        }
    }
}
