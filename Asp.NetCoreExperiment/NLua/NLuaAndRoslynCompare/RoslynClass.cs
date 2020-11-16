using Microsoft.CodeAnalysis;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Globalization;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;
using Smart.Text.Japanese;

namespace NLuaAndRoslynCompare
{
    public class RoslynClass
    {
        public static object Transform(string csharp,params object[] pares)
        {           
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
            // 创建编译任务
            var metadata = GetMetadataReference(typeof(System.Text.RegularExpressions.Regex),typeof(StringExpand), typeof(KanaConverter), typeof(List<>), typeof(IDictionary), typeof(JapaneseCalendar),typeof(CultureInfo),typeof(DateTime),
                 typeof(JsonConvert), typeof(object), typeof(AssemblyTargetedPatchBandAttribute));
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
                    return methodInfo.Invoke(obj, pares)?.ToString();
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
                    list.Add(MetadataReference.CreateFromFile(Assembly.Load(assembly).Location));                  
                }
            }
            return list;
        }
    }
}
