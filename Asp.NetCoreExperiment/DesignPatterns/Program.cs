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
                Console.WriteLine("===================================================");
                Console.WriteLine("1、简单工厂  2、策略模式  3、装饰模式  4、代理模式  5、工厂方法  6、原型模式  7、模板方法  8、外观模式  9、建造者模式  10、观察者模式  11、抽象工厂");
                Console.WriteLine("==================================================");
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
                    case "6":
                        Invock6();
                        break;
                    case "7":
                        Invock7();
                        break;
                    case "8":
                        Invock8();
                        break;
                    case "9":
                        Invock9();
                        break;
                    case "10":
                        Invock10();
                        break;
                    case "11":
                        Invock11();
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
        #region 原型模式客户端  
        static void Invock6()
        {
            ConcretePrototype1 p1 = new ConcretePrototype1("abc");
            ConcretePrototype1 c1 = (ConcretePrototype1)p1.Clone();
            Console.WriteLine($"Cloned:{c1.ID}");
        }
        #endregion
        #region 模板方法模式客户端  
        static void Invock7()
        {
            AbstractClass abC;
            abC = new ConcreteClassA();
            abC.TemplateMethod();

            abC = new ConcreteClassB();
            abC.TemplateMethod();
        }
        #endregion
        #region 外观模式客户端  
        static void Invock8()
        {
            var facade = new Facade();
            facade.FacadeOne();
            facade.FacadeTow();
        }
        #endregion
        #region 建造者模式客户端  
        static void Invock9()
        {
            var shapBuilder = new CircleBuilder();
            var shapeDirector = new ShapeDirector(shapBuilder);
            shapeDirector.DrawShape();
        }
        #endregion
        #region 观察者模式客户端  
        static void Invock10()
        {
            var aObsSub = new AObsSubject();
            aObsSub.Attach(new AObserver(aObsSub));
            aObsSub.Attach(new BObserver(aObsSub));
            aObsSub.Notify();

            var bObsSub = new AObsSubject();
            bObsSub.Attach(new AObserver(bObsSub));
            bObsSub.Attach(new BObserver(bObsSub));
            bObsSub.Attach(new AObserver(bObsSub));
            bObsSub.Notify();
        }
        #endregion

        #region 抽象工厂模式客户端  
        static void Invock11()
        {
            var department = new Department();
            IDataBaseFactory factory = new OracleFactory();// new SqlserverFactory();
            IDepartment departmentOpt = factory.CreateDepartment();
            departmentOpt.Insert(department);
            departmentOpt.GetDepartment(10);

        }
        #endregion
    }
}