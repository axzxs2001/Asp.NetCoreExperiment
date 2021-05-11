using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SourceGeneratorSamples
{
    [Generator]
    public class HelloWorldGenerator : ISourceGenerator
    {      
        
        //必需是.Net Stantard 2.0
        public void Execute(GeneratorExecutionContext context)
        {
            // begin creating the source we'll inject into the users compilation
            var sourceBuilder = new StringBuilder(@"
using System;
namespace HelloWorldGenerated
{
    public static class HelloWorld
    {
        public static void SayHello2() 
        {
            Console.WriteLine(""这是生成的代码"");

");

            // using the context, get a list of syntax trees in the users compilation
            var syntaxTrees = context.Compilation.SyntaxTrees;

            // add the filepath of each tree to the class we're building
            foreach (SyntaxTree tree in syntaxTrees)
            {
                sourceBuilder.AppendLine($@"Console.WriteLine(@""引用程序集- {tree.FilePath}"");");
            }

            // finish creating the source to inject
            sourceBuilder.Append(@"
        }
    }
}");

            // inject the created source into the users compilation
            context.AddSource("HelloWorldGenerated", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

     

        public void Initialize(GeneratorInitializationContext context)
        {
            //
        }
    }
}