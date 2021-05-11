
using System;
namespace Demo01
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
       

                var begin = DateTime.Now;
                // Run the various scenarios
                Console.WriteLine("Running HelloWorld:\n");
                UseHelloWorldGenerator.Run();
                Console.WriteLine((DateTime.Now - begin).TotalMilliseconds);
                Console.ReadLine();
            }
        }
    }


}
