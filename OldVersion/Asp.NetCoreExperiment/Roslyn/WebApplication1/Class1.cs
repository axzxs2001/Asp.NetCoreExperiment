using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Threading.Tasks;

namespace WebApplication1
{
    class Program111
    {


        public static void GetValue()
        {
            var sourceCodeText = @"
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1
{
  public class AAAA : IDynamicController
    {
        [HttpPost]
        public string Add(string s)
        {
            return ""ok"";
        }
    }
}
";

            var syntaxTree = CSharpSyntaxTree.ParseText(sourceCodeText, new CSharpParseOptions(LanguageVersion.Latest)); // 获取代码分析得到的语法树
            var assemblyName = $"WebApplication1";
            // 创建编译任务
            var compilation = CSharpCompilation.Create(assemblyName) //指定程序集名称
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))//输出为 dll 程序集
                .AddReferences(GetMetadataReference(typeof(IDynamicController), typeof(object), typeof(HttpPostAttribute), typeof(AssemblyTargetedPatchBandAttribute))) //添加程序集引用   
                .AddSyntaxTrees(syntaxTree) // 添加上面代码分析得到的语法树     
                ;
            var memory = new MemoryStream();
            var compilationResult = compilation.Emit(memory);
            if (compilationResult.Success)
            {
                try
                {

                    var assembly = Assembly.Load(memory.ToArray());
                    var vvv = assembly.GetModules();
                    Assembly.GetExecutingAssembly().LoadModule("aaaa", memory.ToArray());

                    //var obj = Activator.CreateInstance(type);
                    //var methodInfo = type.GetMethod("Generate");
                    //return methodInfo.Invoke(obj, null)?.ToString();
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



    }
}
