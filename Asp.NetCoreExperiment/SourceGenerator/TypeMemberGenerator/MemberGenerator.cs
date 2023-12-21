using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;
using System.Runtime;
using System.IO;
using System.Reflection;
using System;

namespace TypeMemberGenerator
{
    [Generator]
    public class MemberGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
        }
        public void Execute(GeneratorExecutionContext context)
        {
            try
            {
                var path = GetPath(context);
                AddJsonToCsProj(path);
                var typeModels = new List<TypeModel>();
                foreach (var typeName in GetAllTypeFullNames(context.Compilation))
                {

                    var myType = context.Compilation.Assembly.GetTypeByMetadataName(typeName);
                    if (myType == null)
                    {
                        continue;
                    }

                    var typeModel = new TypeModel();
                    typeModel.TypeName = typeName;
                    typeModel.Properties = new List<string>();
                    typeModel.Types = new List<string>();
                    foreach (var member in myType.GetMembers())
                    {
                        if (member.Kind == SymbolKind.Property)
                        {
                            var pro = member as IPropertySymbol;
                            typeModel.Properties.Add($"{pro?.Name}");
                            typeModel.Types.Add($"{pro?.Type.Name}");
                        }
                    }
                    if (typeModel.Properties.Count > 0)
                    {
                        typeModels.Add(typeModel);
                    }
                }
                SavaJson(typeModels, path = path + "\\typemember.json");
            }
            catch (Exception exc)
            {
                File.WriteAllText(@"C:\MyFile\temp\error.txt", exc.Message);
            }

        }
        string GetPath(GeneratorExecutionContext context)
        {
            var path = "";
            var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);
            if (mainMethod != null)
            {
                foreach (var location in mainMethod.ContainingModule.Locations)
                {
                    if (location.GetLineSpan().Path.ToLower().Contains("program.cs"))
                    {
                        path = Path.GetDirectoryName(location.GetLineSpan().Path);
                        break;
                    }
                }
            }
            return path;
        }
        void SavaJson(List<TypeModel> typeModels, string filePath)
        {
            var jsonSB = new StringBuilder();
            jsonSB.AppendLine("[");
            foreach (var typeModel in typeModels)
            {
                jsonSB.AppendLine("   {");
                jsonSB.AppendLine($"      \"TypeName\":\"{typeModel.TypeName}\",");
                jsonSB.AppendLine($"      \"Properties\":[");
                foreach (var property in typeModel.Properties)
                {
                    jsonSB.AppendLine($"         \"{property}\",");
                }
                jsonSB.Remove(jsonSB.Length - 3, 1);
                jsonSB.AppendLine("      ],");
                jsonSB.AppendLine($"      \"Types\":[");
                foreach (var t in typeModel.Types)
                {
                    jsonSB.AppendLine($"         \"{t}\",");
                }
                jsonSB.Remove(jsonSB.Length - 3, 1);
                jsonSB.AppendLine("      ],");
                jsonSB.Remove(jsonSB.Length - 3, 1);
                jsonSB.AppendLine("   },");
            }
            jsonSB.Remove(jsonSB.Length - 3, 1);
            jsonSB.AppendLine("]");
            File.WriteAllText(filePath, jsonSB.ToString());
        }

        void AddJsonToCsProj(string path)
        {
            var content = """
                	<ItemGroup>
                  		<None Remove="typemember.json" />
                	</ItemGroup>
                	<ItemGroup>
                  		<EmbeddedResource Include="typemember.json" />
                	</ItemGroup>
                """
            ;

            foreach (var filePath in Directory.GetFiles(path, "*.csproj"))
            {
                if (!File.ReadAllText(filePath).Contains("typemember.json"))
                {
                    var list = new List<string>(File.ReadAllLines(filePath));
                    list.Insert(list.Count - 1, content);
                    File.WriteAllLines(filePath, list);
                }
            }
        }


        /// <summary>
        /// 获取类型的全名
        /// </summary>
        /// <param name="compilation"></param>
        /// <returns></returns>
        IEnumerable<string> GetAllTypeFullNames(Compilation compilation)
        {
            List<string> fullNames = new List<string>();

            foreach (var tree in compilation.SyntaxTrees)
            {
                var semanticModel = compilation.GetSemanticModel(tree);
                var typeDeclarations = tree.GetRoot().DescendantNodes().OfType<TypeDeclarationSyntax>();

                foreach (var typeDecl in typeDeclarations)
                {
                    var symbol = semanticModel.GetDeclaredSymbol(typeDecl);
                    if (symbol != null)
                    {
                        fullNames.Add(GetFullMetadataName(symbol));
                    }
                }
            }
            return fullNames;
            string GetFullMetadataName(ISymbol s)
            {
                if (s == null || IsRootNamespace(s))
                {
                    return string.Empty;
                }

                var sb = new System.Text.StringBuilder(s.MetadataName);
                var last = s;

                s = s.ContainingSymbol;

                while (!IsRootNamespace(s))
                {
                    if (s is ITypeSymbol && last is ITypeSymbol)
                    {
                        sb.Insert(0, '+');
                    }
                    else
                    {
                        sb.Insert(0, '.');
                    }

                    sb.Insert(0, s.MetadataName);
                    last = s;
                    s = s.ContainingSymbol;
                }

                return sb.ToString();
            }
            bool IsRootNamespace(ISymbol symbol)
            {
                return symbol is INamespaceSymbol n && n.IsGlobalNamespace;
            }
        }
    }
    class TypeModel
    {
        public string TypeName { get; set; }
        public List<string> Properties { get; set; }
        public List<string> Types { get; set; }
    }
}
