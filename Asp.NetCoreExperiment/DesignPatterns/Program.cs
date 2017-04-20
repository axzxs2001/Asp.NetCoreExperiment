using System;
using System.Text;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            while (true)
            {
                Console.WriteLine("==================================");
                Console.WriteLine("1、简单工厂  2、策略模式  3、装饰模式");
                Console.WriteLine("==================================");
                Console.WriteLine("选择模式编号：");
                switch (Console.ReadLine())
                {
                    case "1":
                        Invock1();
                        break;
                    case "2":
                        Invock2();
                        break;
                    case "3":
                        Invock3();
                        break;
                }
            }
        }
        /// <summary>
        /// 简单工厂客户端
        /// </summary>
        static void Invock1()
        {
            var opertion = OperationFactory.CreateOperate(1);
            opertion.GetResult();
        }
        /// <summary>
        /// 策略模式客户端
        /// </summary>
        static void Invock2()
        {
            var context = new Context("1");
            context.GetCompute();
        }
        /// <summary>
        /// 装饰模式客户端
        /// </summary>
        static void Invock3()
        {
            var c = new ConcreteComponent();
            var dA = new ConcreteDecoratorA();
            var dB = new ConcreteDecoratorB();
            dA.Component = c;
            dB.Component = dA;
            dB.Operation();
        }
    }
}