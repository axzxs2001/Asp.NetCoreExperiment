using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static System.Console;
using SourceGeneratorDemo03;
using System.Collections;

//F01();
F02();

void F00()
{


    NameSyntax name = IdentifierName("System");
    WriteLine($"\tCreated the identifier {name}");

    name = QualifiedName(name, IdentifierName("Collections"));
    WriteLine(name.ToString());

    name = QualifiedName(name, IdentifierName("Generic"));
    WriteLine(name.ToString());
}



void F01()
{
    NameSyntax name = IdentifierName("System");
    name = QualifiedName(name, IdentifierName("Collections"));  
    name = QualifiedName(name, IdentifierName("Generic"));
    const string sampleCode =
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
    var tree = CSharpSyntaxTree.ParseText(sampleCode);
    var root = (CompilationUnitSyntax)tree.GetRoot();

    var oldUsing = root.Usings[1];
    var newUsing = oldUsing.WithName(name);
    WriteLine(root.ToString());

    root = root.ReplaceNode(oldUsing, newUsing);
    WriteLine(root.ToString());
}

void F02()
{
    var test = CreateTestCompilation();
    foreach (SyntaxTree sourceTree in test.SyntaxTrees)
    {
        var model = test.GetSemanticModel(sourceTree);

        var rewriter = new TypeInferenceRewriter(model);

        var newSource = rewriter.Visit(sourceTree.GetRoot());

        if (newSource != sourceTree.GetRoot())
        {
            File.WriteAllText(sourceTree.FilePath, newSource.ToFullString());
        }    
    }


}

Compilation CreateTestCompilation()
{
    String programPath = @"..\..\..\Program.cs";
    String programText = File.ReadAllText(programPath);
    var programTree =
                   CSharpSyntaxTree.ParseText(programText)
                                   .WithFilePath(programPath);

    String rewriterPath = @"..\..\..\TypeInferenceRewriter.cs";
    String rewriterText = File.ReadAllText(rewriterPath);
    var rewriterTree =
                   CSharpSyntaxTree.ParseText(rewriterText)
                                   .WithFilePath(rewriterPath);

    SyntaxTree[] sourceTrees = { programTree, rewriterTree };

    MetadataReference mscorlib =
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
    MetadataReference codeAnalysis =
            MetadataReference.CreateFromFile(typeof(SyntaxTree).Assembly.Location);
    MetadataReference csharpCodeAnalysis =
            MetadataReference.CreateFromFile(typeof(CSharpSyntaxTree).Assembly.Location);

    MetadataReference[] references = { mscorlib, codeAnalysis, csharpCodeAnalysis };

    return CSharpCompilation.Create("TransformationCS",
        sourceTrees,
        references,
        new CSharpCompilationOptions(OutputKind.ConsoleApplication));
}

