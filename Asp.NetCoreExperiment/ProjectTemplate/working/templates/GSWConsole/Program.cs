using System;
using Dapper;
using NLog;

namespace GSWCon
{
    class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine(Dapper.CommandFlags.Buffered);
        Console.WriteLine(NLog.Config.ExceptionRenderingFormat.Data);
    }
}
}
