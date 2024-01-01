using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DapperAOTGenerator
{
    [Generator]
    public class MockDapperGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            // 注册一个语法树遍历器，用于在语法树中查找目标方法调用
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            // 获取 SyntaxReceiver 实例
            if (!(context.SyntaxReceiver is SyntaxReceiver syntaxReceiver))
                return;
            // 获取编译时的语法树
            var compilation = context.Compilation;
            var list = new List<(string FilePath, int Line, int Column)>();
            // 遍历语法树
            foreach (var syntaxTree in compilation.SyntaxTrees)
            {
                // 获取语法树的根节点
                var root = syntaxTree.GetRoot();
                // 获取所有符合条件的方法调用节点
                var methodCalls = syntaxReceiver.MethodCalls;
                foreach (var methodCall in methodCalls)
                {
                    // 获取调用方的文件路径
                    var filePath = syntaxTree.FilePath;
                    try
                    {
                        // 获取调用方的行号和列号
                        var position = syntaxTree.GetLineSpan(methodCall.Span);
                        var line = position.StartLinePosition.Line + 1;
                        var column = position.StartLinePosition.Character + 1 + methodCall.GetText().ToString().IndexOf("Query");
                        // 获取方法调用的符号信息
                        var methodSymbol = compilation.GetSemanticModel(syntaxTree).GetSymbolInfo(methodCall).Symbol as IMethodSymbol;

                        if (methodSymbol != null && methodSymbol.IsExtensionMethod && methodSymbol.ReducedFrom != null)
                        {
                            // 获取调用方的类型
                            var callingType = methodSymbol.ReducedFrom.ReceiverType;

                            if (callingType != null)
                            {
                                // 判断是否是 Dapper 的扩展方法
                                if (callingType.Name == "SqlMapper")
                                {
                                    File.AppendAllText(@"C:\MyFile\temp\error.txt", filePath + "\r\n");
                                    if (!filePath.Contains("/obj/") && !filePath.Contains("\\obj\\"))
                                    {
                                        list.Add((filePath, line, column));
                                    }
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
            var sourse = BuildSourse(list);
            context.AddSource("DapperAOTAPITest.g.cs", sourse);
        }

        string BuildSourse(IEnumerable<(string FilePath, int Line, int Column)> lines)
        {
            var codes = new StringBuilder();
            foreach (var line in lines)
            {
                codes.AppendLine($"[InterceptsLocation(@\"{line.FilePath}\", {line.Line}, {line.Column})]");
            }
            var source = $$"""
                         using System;
                         using System.Data;
                         using System.Runtime.CompilerServices;
                       
                         namespace DapperAOTAPITest.Interceptor
                         {
                            public static class DapperInterceptor
                            {                            
                               {{codes.ToString().Trim('\r', '\n')}}                             
                               public static IEnumerable<T> InterceptorQuery<T>(this IDbConnection cnn,string sql,object? param=null, IDbTransaction? transaction=null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
                               {
                                   var message=$"这是Query拦截器 {sql}";
                                   Console.WriteLine(message);
                                   throw new Exception(message);
                               }
                            }
                         }
                         """;
            return source;
        }

        // SyntaxReceiver 用于收集方法调用的信息
        class SyntaxReceiver : ISyntaxReceiver
        {
            public List<InvocationExpressionSyntax> MethodCalls { get; } = new List<InvocationExpressionSyntax>();

            public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
            {
                // 在这里添加你的语法节点匹配逻辑，以收集目标方法调用的信息
                if (syntaxNode is InvocationExpressionSyntax invocationSyntax &&
                    invocationSyntax.Expression is MemberAccessExpressionSyntax memberAccessSyntax &&
                    memberAccessSyntax.Name.Identifier.ValueText == "Query")
                {
                    MethodCalls.Add(invocationSyntax);
                }
            }        
        }      
    }
}

