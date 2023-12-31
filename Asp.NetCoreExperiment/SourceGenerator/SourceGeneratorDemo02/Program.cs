

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

F01();
void F01()
{
    const string programText =
@"using System;
using System.Collections.Generic;
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

    var compilation = CSharpCompilation.Create("HelloWorld")
        .AddReferences(MetadataReference.CreateFromFile(
            typeof(string).Assembly.Location))
        .AddSyntaxTrees(tree);

    SemanticModel model = compilation.GetSemanticModel(tree);

    // Use the syntax tree to find "using System;"
    UsingDirectiveSyntax usingSystem = root.Usings[0];
    NameSyntax systemName = usingSystem.Name;

    // Use the semantic model for symbol information:
    SymbolInfo nameInfo = model.GetSymbolInfo(systemName);

    var systemSymbol = (INamespaceSymbol?)nameInfo.Symbol;
    if (systemSymbol?.GetNamespaceMembers() is not null)
    {
        foreach (INamespaceSymbol ns in systemSymbol?.GetNamespaceMembers()!)
        {
            Console.WriteLine(ns);
        }
    }

    // Use the syntax model to find the literal string:
    LiteralExpressionSyntax helloWorldString = root.DescendantNodes()
    .OfType<LiteralExpressionSyntax>()
    .Single();

    // Use the semantic model for type information:
    TypeInfo literalInfo = model.GetTypeInfo(helloWorldString);

    var stringTypeSymbol = (INamedTypeSymbol?)literalInfo.Type;

    var allMembers = stringTypeSymbol?.GetMembers();

    var methods = allMembers?.OfType<IMethodSymbol>();
    var publicStringReturningMethods = methods?
    .Where(m => SymbolEqualityComparer.Default.Equals(m.ReturnType, stringTypeSymbol) &&
    m.DeclaredAccessibility == Accessibility.Public);

    var distinctMethods = publicStringReturningMethods?.Select(m => m.Name).Distinct();

    foreach (string name in (from method in stringTypeSymbol?
                         .GetMembers().OfType<IMethodSymbol>()
                             where SymbolEqualityComparer.Default.Equals(method.ReturnType, stringTypeSymbol) &&
                             method.DeclaredAccessibility == Accessibility.Public
                             select method.Name).Distinct())
    {
        Console.WriteLine(name);
    }





}