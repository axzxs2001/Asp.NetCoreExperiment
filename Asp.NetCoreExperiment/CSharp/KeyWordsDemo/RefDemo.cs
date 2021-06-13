using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Sigil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KeyWordsDemo
{
    class RefDemo : IDemo
    {
        public void Run()
        {
            BenchmarkRunner.Run<ReflectionBenchmarks>();
            // var value = RefDemo.TraditionlReflection();

        }

        public static string SimpleGet()
        {

            var someClass = new VeryPublicClass();
            return someClass.VeryPublicProperty;
        }

        public static string TraditionlReflection()
        {
            var someClass = new VeryPublicClass();
            var propertyInfo = someClass.GetType().GetProperty("VeryPrivateProperty", BindingFlags.Instance | BindingFlags.NonPublic);
            var value = propertyInfo!.GetValue(someClass);
            return value!.ToString();
        }

        private static readonly PropertyInfo CachedProperty = typeof(VeryPublicClass).GetProperty("VeryPrivateProperty", BindingFlags.Instance | BindingFlags.NonPublic);
        public static string OptimizedTraditionalReflection()
        {
            var someClass = new VeryPublicClass();
            var value = CachedProperty!.GetValue(someClass);
            return value!.ToString();
        }


        private readonly static Func<VeryPublicClass, string> GetPropertyDelegate = (Func<VeryPublicClass, string>)Delegate.CreateDelegate(typeof(Func<VeryPublicClass, string>), CachedProperty.GetGetMethod(true)!);
        public static string CompliledDelegate()
        {
            var someClass = new VeryPublicClass();
            return GetPropertyDelegate(someClass);

        }

        private static readonly PropertyInfo CachedProperty1 = typeof(VeryInternalClass).GetProperty("VeryPrivateProperty", BindingFlags.Instance | BindingFlags.NonPublic);
        private readonly static Func<VeryInternalClass, string> GetPropertyDelegate1 = (Func<VeryInternalClass, string>)Delegate.CreateDelegate(typeof(Func<VeryInternalClass, string>), CachedProperty1.GetGetMethod(true)!);
        public static string CompliledDelegate1()
        {
            var someClass = new VeryInternalClass();
            return GetPropertyDelegate1(someClass);

        }



        private static readonly Type VeryInternalClassType = Type.GetType("KeyWordsDemo.VeryInternalClass,KeyWordsDemo");
        private static readonly PropertyInfo CachedInternalProperty = VeryInternalClassType.GetProperty("VeryPrivateProperty", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly Emit<Func<object, string>> GetPropertyEmitter = Emit<Func<object, string>>
            .NewDynamicMethod("GetInternalPropertyValue")
            .LoadArgument(0)
            .CastClass(VeryInternalClassType)
            .Call(CachedInternalProperty.GetGetMethod(true))
            .Return();

        private static readonly Func<object, string> GetPropertyEmittedDelegate = GetPropertyEmitter.CreateDelegate();

        public static string EmittedIlVersion()
        {
            var internalClass = Activator.CreateInstance(VeryInternalClassType);
            return GetPropertyEmittedDelegate(internalClass);
        }
    }

    [MemoryDiagnoser]
    public class ReflectionBenchmarks
    {
        [Benchmark]
        public string SimpleGet() => RefDemo.SimpleGet();

        //[Benchmark]
        //public string TraditionlReflection() => RefDemo.TraditionlReflection();


        //[Benchmark]
        //public string OptimizedTraditionalReflection() => RefDemo.OptimizedTraditionalReflection();


        //[Benchmark]
        //public string CompliledDelegate() => RefDemo.CompliledDelegate();


        //[Benchmark]
        //public string CompliledDelegate1() => RefDemo.CompliledDelegate1();

        [Benchmark]
        public string EmittedIlVersion() => RefDemo.EmittedIlVersion();


    }

    public class VeryPublicClass
    {
        private string VeryPrivateProperty { get; set; } = "Default";

        public string VeryPublicProperty { get; set; } = "Default";
    }

    internal class VeryInternalClass
    {
        private string VeryPrivateProperty { get; set; } = "Default";

        public string VeryPublicProperty { get; set; } = "Default";
    }
}
