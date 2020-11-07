using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;
using RoslynDemo001_1;
using Smart.Text.Japanese;

namespace RoslynDemo001
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("按e退出，任意键继续！");
                var key = Console.ReadLine();
                if (key.ToLower() == "e")
                {
                    break;
                }
                var t = new Thread(GetValue);
                t.Start();
                //Console.WriteLine(GetValue());
            }

        }

        static void GetValue()
        {


            var csharp = @"var tel = par as string;
            if (!string.IsNullOrEmpty(tel))
            {
                tel = tel.Replace(""-"", """");
                if (tel.Length == 10)
            {
                tel = tel.Insert(6, ""-"").Insert(3, ""-"");
            }
            else if (tel.Length == 11)
            {
                tel = tel.Insert(7, ""-"").Insert(3, ""-"");
            }
        }
            return tel;";

            var sourceCodeText = $@"
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Smart.Text.Japanese;
using System.Globalization;


namespace RoslynDynamicGenerate
{{
    public class DynamicGenerateClass
    {{
        public object Generate(object par)
        {{                
          {csharp}
        }}      
    }}
}}
";



            var syntaxTree = CSharpSyntaxTree.ParseText(sourceCodeText, new CSharpParseOptions(LanguageVersion.Latest)); // 获取代码分析得到的语法树
            var assemblyName = $"RoslynDynamicGenerate";
            var metadata = GetMetadataReference(typeof(System.Text.RegularExpressions.Regex), typeof(StringExpand), typeof(KanaConverter), typeof(List<>), typeof(IDictionary), typeof(JapaneseCalendar), typeof(CultureInfo), typeof(DateTime),
                       typeof(JsonConvert), typeof(object), typeof(AssemblyTargetedPatchBandAttribute));


            // 创建编译任务
            var compilation = CSharpCompilation.Create(assemblyName) //指定程序集名称
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))//输出为 dll 程序集
                .AddReferences(metadata) //添加程序集引用   
                .AddSyntaxTrees(syntaxTree) // 添加上面代码分析得到的语法树     
                ;
            var memory = new MemoryStream();
            var compilationResult = compilation.Emit(memory);
            if (compilationResult.Success)
            {
                try
                {
                    var assembly = Assembly.Load(memory.ToArray());
                    var type = assembly.GetType("RoslynDynamicGenerate.DynamicGenerateClass");
                    var obj = Activator.CreateInstance(type);
                    var methodInfo = type.GetMethod("Generate");
                    Thread.Sleep(1000);
                    Console.WriteLine(methodInfo.Invoke(obj, new object[] { "06-6809-3322" })?.ToString());
                }
                finally
                {
                    memory.Close();
                }
          
            }
            else
            {
                foreach (var diagnositic in compilationResult.Diagnostics)
                {
                    Console.WriteLine(diagnositic);
                }
                throw new ApplicationException($"下面C#语句有语法错误{sourceCodeText}");
            }
        }
        /// <summary>
        /// 从类型获取原数据引用
        /// </summary>
        /// <param name="types">类型集合</param>
        /// <returns></returns>
        static List<MetadataReference> GetMetadataReference(params Type[] types)
        {
            var list = new List<MetadataReference>();
            foreach (var type in types)
            {
                var metadateRef = MetadataReference.CreateFromFile(type.Assembly.Location);
                list.Add(metadateRef);
                foreach (var assembly in type.Assembly.GetReferencedAssemblies())
                {
                    //Console.WriteLine("-------------");
                    //Console.WriteLine(assembly.FullName);
                    list.Add(MetadataReference.CreateFromFile(Assembly.Load(assembly).Location));
                    //Console.WriteLine("-------------");
                }
            }
            return list;
        }


        static void CompilationAssemblly()
        {
            var sourceCodeText = @"
using System;
using System.Collections.Generic;
using System.Text;

namespace RoslynDemo001_1
{
    public class TestClass
    {
        public string A { get; set; }
        public double B { get; set; }
    }
}
";

            var systemReference = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceCodeText, new CSharpParseOptions(LanguageVersion.Latest)); // 获取代码分析得到的语法树
            var assemblyName = $"gsw";
            // 创建编译任务
            var compilation = CSharpCompilation.Create(assemblyName) //指定程序集名称
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))//输出为 dll 程序集
                .AddReferences(systemReference) //添加程序集引用
                .AddSyntaxTrees(syntaxTree) // 添加上面代码分析得到的语法树
                ;
            var assemblyPath = $"C:/MyFile/gsw.dll";
            //文件方式
            // var compilationResult = compilation.Emit(assemblyPath); // 执行编译任务，并输出编译后的程序集
            var memory = new MemoryStream();
            var compilationResult = compilation.Emit(memory);
            if (compilationResult.Success)
            {
                try
                {
                    // byte[] assemblyByte;                  
                    //using (var fs = File.OpenRead(assemblyPath))
                    //{
                    //    assemblyBytes = new byte[fs.Length];
                    //    fs.Read(assemblyBytes, 0, assemblyBytes.Length);
                    //}
                    //memory.Close();
                    var assembly = Assembly.Load(memory.ToArray());
                    Console.WriteLine(assembly);
                    foreach (var type in assembly.GetTypes())
                    {
                        Console.WriteLine(type.Name);
                    }
                }
                finally
                {
                    File.Delete(assemblyPath);
                }
            }
        }
    }
}
