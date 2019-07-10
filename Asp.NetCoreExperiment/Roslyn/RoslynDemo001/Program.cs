using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Reflection;
using System.Web.Http.Dependencies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;


namespace RoslynDemo001
{
    class Program
    {
        static void Main(string[] args)
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
            Console.WriteLine("Hello World!");
        }       
    }
}
