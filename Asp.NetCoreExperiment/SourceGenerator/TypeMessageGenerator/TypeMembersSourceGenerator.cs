using Microsoft.CodeAnalysis;
using System.IO;
using System.Reflection;

namespace TypeMessageGenerator
{
    [Generator]
    public class TypeMembersSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            try
            {
                var path = "";
                var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);
                foreach (var location in mainMethod.ContainingModule.Locations)
                {
                    if (location.GetLineSpan().Path.ToLower().Contains("program.cs"))
                    {
                        path = Path.GetDirectoryName(location.GetLineSpan().Path);
                        break;
                    }                    
                }
                var content = """
                	<ItemGroup>
                  		<None Remove="type.txt" />
                	</ItemGroup>
                	<ItemGroup>
                  		<EmbeddedResource Include="type.txt" />
                	</ItemGroup>
                """;

                foreach (var filePath in Directory.GetFiles(path, "*.csproj"))
                {
                    if (!File.ReadAllText(filePath).Contains("type.txt"))
                    {
                        var list = new List<string>(File.ReadAllLines(filePath));
                        list.Insert(list.Count - 1, content);
                        File.WriteAllLines(filePath, list);
                    }
                }


                path = path + "\\type.txt";
                File.WriteAllText(path, "");
                File.AppendAllText(path, context.Compilation.Assembly.Name + "\r\n");
                File.AppendAllText(path, "========================\r\n");

                foreach (var typename in context.Compilation.Assembly.TypeNames)
                {
                    File.AppendAllText(path, typename + "\r\n");
                    try
                    {
                        var mytype = context.Compilation.Assembly.GetTypeByMetadataName(typename);
                        if (mytype == null)
                        {
                            mytype = context.Compilation.Assembly.GetTypeByMetadataName($"{context.Compilation.Assembly.Name}.{typename}");
                        }
                        if (mytype == null)
                        {
                            continue;
                        }

                        foreach (var member in mytype.GetMembers())
                        {
                            try
                            {

                                File.AppendAllText(path, $"-- {member.Name} {member.Kind} \r\n");
                                foreach (var location in member.Locations)
                                {
                                    try
                                    {

                                        File.AppendAllText(path, $"---- {location.GetLineSpan().StartLinePosition.Line},{location.GetLineSpan().EndLinePosition.Character} {location.GetLineSpan().Path}\r\n");
                                    }
                                    catch (Exception exc)
                                    {
                                        File.AppendAllText(path, $"1   {exc.Message} \r\n");
                                    }
                                }
                            }
                            catch (Exception exc)
                            {
                                File.AppendAllText(path, $"2   {exc.Message} \r\n");
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        File.AppendAllText(path, $"3   {exc.Message} \r\n");
                    }
                }
            }
            catch (Exception exc)
            {
                File.AppendAllText(@"C:\MyFile\temp\error.txt", $" {exc.Message} \r\n");
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {


        }
    }
}
