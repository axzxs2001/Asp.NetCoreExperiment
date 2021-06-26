using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Demo01
{
    [MemoryDiagnoser]
    class MethodDemo : IDemo
    {
        public void Run()
        {
            BenchmarkRunner.Run<TestMethod>();
        }
    }
    public class TestMethod
    {
        private readonly MyClass _myClass;
        private readonly Func<MyClass, string> _delegate;
        private readonly MethodInfo _methodinfo;

        public TestMethod()
        {
            _myClass = new MyClass();
            _methodinfo = _myClass.GetType().GetMethod("MyMethod");
            _delegate = (Func<MyClass, string>)Delegate.CreateDelegate(typeof(Func<MyClass, string>), _methodinfo);
        }

        [Benchmark]
        public string MethodA()
        {
            return _myClass.MyMethod();
        }
        [Benchmark]
        public string MethodAExt()
        {
            var myClass = new MyClass();
            return myClass.MyMethod();
        }

        [Benchmark]
        public string MethodB()
        {
            return _methodinfo.Invoke(_myClass, new object[0]).ToString();
        }
        [Benchmark]
        public string MethodBExt()
        {
            var myClass = new MyClass();
            var methodinfo = _myClass.GetType().GetMethod("MyMethod");
            return methodinfo.Invoke(myClass, new object[0]).ToString();
        }
        [Benchmark]
        public string MethodC()
        {
            return _delegate(_myClass);
        }
        [Benchmark]
        public string MethodCExt()
        {
            var myClass = new MyClass();
            var methodinfo = myClass.GetType().GetMethod("MyMethod");
            var dele = (Func<MyClass, string>)Delegate.CreateDelegate(typeof(Func<MyClass, string>), methodinfo);
            return dele(myClass);
        }

       // [Benchmark]
        //public string MethodD()
        //{
        //    // 创建一个程序集构建器            
        //    var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("Demo01"), AssemblyBuilderAccess.Run);
        //    // 使用程序集构建器创建一个模块构建器
        //    var moduleBuilder = assemblyBuilder.DefineDynamicModule("Demo01");
        //    // 使用模块构建器创建一个类型构建器
        //    var typeBuilder = moduleBuilder.DefineType("Demo01.TClass", TypeAttributes.Public);

        //    // 使用类型构建器创建一个方法构建器
        //    var methodBuilder = typeBuilder.DefineMethod("MyMethod", MethodAttributes.Public, typeof(string), null);
        //    // 通过方法构建器获取一个MSIL生成器
        //    var IL = methodBuilder.GetILGenerator();
        //    // 开始编写方法的执行逻辑  
        //    IL.Emit(OpCodes.Call, typeof(TClass).GetMethod("MyMethod"));
        //    IL.Emit(OpCodes.Nop);
        //    // 退出函数
        //    IL.Emit(OpCodes.Ret);
        //    //方法结束
        //    // 从类型构建器中创建出类型
        //    var dynamicType = typeBuilder.CreateType();
        //    // 通过反射创建出动态类型的实例
        //    var tClass = Activator.CreateInstance(dynamicType);
        //    var myMethod = dynamicType.GetMethod("MyMethod");
        //    return myMethod.Invoke(tClass,new object[0]).ToString();


        //}
    }

    public class TClass
    {
        public string MyMethod()
        {
            return "test";
        }
    }
}
