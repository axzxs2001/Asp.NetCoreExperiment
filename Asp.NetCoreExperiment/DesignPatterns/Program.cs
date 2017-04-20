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
                Console.WriteLine("1、简单工厂  2、策略模式  3、装饰模式  4、代理模式  5、工厂方法");
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
                    case "4":
                        Invock4();
                        break;
                    case "5":
                        Invock5();
                        break;
                }
            }
        }
        #region 简单工厂客户端      
        static void Invock1()
        {
            var opertion = OperationFactory.CreateOperate(1);
            opertion.GetResult();
        }
        #endregion
        #region 策略模式客户端    
        static void Invock2()
        {
            var context = new Context("1");
            context.GetCompute();
        }
        #endregion
        #region 装饰模式客户端   
        static void Invock3()
        {
            var c = new ConcreteComponent();
            var dA = new ConcreteDecoratorA();
            var dB = new ConcreteDecoratorB();
            dA.Component = c;
            dB.Component = dA;
            dB.Operation();
        }
        #endregion
        #region 代理模式客户端  
        static void Invock4()
        {
            var proxy = new Proxy();
            proxy.Request();
        }
        #endregion
        #region 工厂方法客户端  
        static void Invock5()
        {
            IFactory funFactoryA = new FactoryA();
            Function functionA = funFactoryA.CreateFuntion();
            functionA.Operation();

            IFactory funFactoryB = new FactoryB();
            Function functionB = funFactoryB.CreateFuntion();
            functionB.Operation();
        }
        #endregion
    }
}