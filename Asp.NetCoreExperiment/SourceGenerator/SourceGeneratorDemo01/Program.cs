using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

F2();


void F1()
{

    Console.WriteLine("-----------------------");
    const string programText =
    @"using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""Hello, World!"");
        }
    }
}";
    SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
    CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
    Console.WriteLine($"这个树是一个 {root.Kind()} 节点");
    Console.WriteLine($"这个树有 {root.Members.Count} 个元素");
    Console.WriteLine($"这个树有 {root.Usings.Count} using 元素. 它们是:");
    foreach (UsingDirectiveSyntax element in root.Usings)
    {
        Console.WriteLine($"\t{element.Name}");
    }
    MemberDeclarationSyntax firstMember = root.Members[0];
    Console.WriteLine($"第一个成员是一个 {firstMember.Kind()}");
    var helloWorldDeclaration = (NamespaceDeclarationSyntax)firstMember;

    Console.WriteLine($"有 {helloWorldDeclaration.Members.Count}个成员在这个 namespace里");
    Console.WriteLine($"第一个成员是一个 {helloWorldDeclaration.Members[0].Kind()}.");

    var programDeclaration = (ClassDeclarationSyntax)helloWorldDeclaration.Members[0];
    Console.WriteLine($"有 {programDeclaration.Members.Count} 成成员定义在 {programDeclaration.Identifier} class里");
    Console.WriteLine($"第一个成员是 {programDeclaration.Members[0].Kind()}");
    var mainDeclaration = (MethodDeclarationSyntax)programDeclaration.Members[0];

    Console.WriteLine($"{mainDeclaration.Identifier} 方法的返回值类型是 {mainDeclaration.ReturnType}");
    Console.WriteLine($"这个方法有 {mainDeclaration.ParameterList.Parameters.Count} 个参数");
    foreach (ParameterSyntax item in mainDeclaration.ParameterList.Parameters)
    {
        Console.WriteLine($"参数{item.Identifier} 的类型是 {item.Type}.");
    }
    Console.WriteLine($"{mainDeclaration.Identifier} 的方法体是:");
    Console.WriteLine(mainDeclaration.Body?.ToFullString());

    Console.WriteLine("-----------------------");

    var argsParameter = mainDeclaration.ParameterList.Parameters[0];
    var firstParameters = from methodDeclaration in root.DescendantNodes()
                                            .OfType<MethodDeclarationSyntax>()
                          where methodDeclaration.Identifier.ValueText == "Main"
                          select methodDeclaration.ParameterList.Parameters.First();

    var argsParameter2 = firstParameters.Single();

    Console.WriteLine(argsParameter == argsParameter2);

}

void F2()
{
    const string programText =
   @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace TopLevel
{
    using Microsoft;
    using System.ComponentModel;

    namespace Child1
    {
        using Microsoft.Win32;
        using System.Runtime.InteropServices;

        class Foo {
             public void F(){};
        }
    }

    namespace Child2
    {
        using System.CodeDom;
        using Microsoft.CSharp;

        class Bar {
             public void F(){};
        }
    }
}";
    SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
    CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
    var collector = new UsingCollector();
    collector.Visit(root);
    //foreach (var directive in collector.Usings)
    //{
    //    Console.WriteLine(directive.Name);
    //}

    //Console.WriteLine("-----------------------");
    foreach (var directive in collector.Methods)
    {
        Console.WriteLine($"{directive.Identifier.ValueText}");
        foreach (var n in directive.Ancestors(true))
        {
            Console.WriteLine($"{n.ToFullString()}");
            Console.WriteLine("----------------");
        }
        Console.WriteLine("=============================");
    }

}
class UsingCollector : CSharpSyntaxWalker
{
    public ICollection<UsingDirectiveSyntax> Usings { get; } = new List<UsingDirectiveSyntax>();
    //public override void VisitUsingDirective(UsingDirectiveSyntax node)
    //{
    //    Console.WriteLine($"\tVisitUsingDirective called with {node.Name}.");
    //    if (node?.Name?.ToString() != "System" && !node.Name.ToString().StartsWith("System."))
    //    {
    //        Console.WriteLine($"\t\tSuccess. Adding {node.Name}.");
    //        this.Usings.Add(node);
    //    }
    //}

    public ICollection<MethodDeclarationSyntax> Methods { get; } = new List<MethodDeclarationSyntax>();
    public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        Console.WriteLine($"\tVisitMethodDeclaration called with {node.Identifier.Text}.");
        this.Methods.Add(node);
        base.VisitMethodDeclaration(node);
    }
}