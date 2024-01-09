using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceGeneratorDemo03
{
    public class TypeInferenceRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel SemanticModel;

        public TypeInferenceRewriter(SemanticModel semanticModel) => SemanticModel = semanticModel;

        public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            if (node.Declaration.Variables.Count > 1)
            {
                return node;
            }
            if (node.Declaration.Variables[0].Initializer == null)
            {
                return node;
            }

            var declarator = node.Declaration.Variables.First();
            var variableTypeName = node.Declaration.Type;

            var variableType = (ITypeSymbol)SemanticModel
                .GetSymbolInfo(variableTypeName)
                .Symbol;


            var initializerInfo = SemanticModel.GetTypeInfo(declarator.Initializer.Value);


            if (SymbolEqualityComparer.Default.Equals(variableType, initializerInfo.Type))
            {
                TypeSyntax varTypeName = SyntaxFactory.IdentifierName("var")
                    .WithLeadingTrivia(variableTypeName.GetLeadingTrivia())
                    .WithTrailingTrivia(variableTypeName.GetTrailingTrivia());

                return node.ReplaceNode(variableTypeName, varTypeName);
            }
            else
            {
                return node;
            }


        }


    }
}
