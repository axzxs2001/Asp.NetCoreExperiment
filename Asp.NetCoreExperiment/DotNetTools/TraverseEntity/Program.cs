using System;
using System.Reflection;

namespace TraverseEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                var versionString = Assembly.GetEntryAssembly()
                                        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                        .InformationalVersion
                                        .ToString();
                //Usage: dotnet[options]
                //Usage: dotnet[path - to - application]

                //Options:
                //                -h | --help         Display help.
                //  --info            Display.NET information.
                //  --list - sdks       Display the installed SDKs.
                //  --list - runtimes   Display the installed runtimes.

                //path - to - application:
                //The path to an application.dll file to execute.
                Console.WriteLine($"TraverseEntity v{versionString}");
                Console.WriteLine("-------------");
                Console.WriteLine("\nUsage:");
                Console.WriteLine("  trav [options]");
                Console.WriteLine("-------------");
                Console.WriteLine("支持的数据库有:");
                Console.WriteLine("  mysql");
                Console.WriteLine("  mssql");
                Console.WriteLine("  postgres");
                return;
            }

            if (args.Length > 1)
            {
                switch (args[0])
                {
                    case "-t":

                        break;
                }
            }

        }
    }
}
