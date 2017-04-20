using System;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("选择模式编号：");
            switch (Console.ReadLine())
            {
                case "1":
                    var opertion = OperationFactory.CreateOperate(1);
                    opertion.GetResult();
                    break;
            }
        }
    }
}