using Microsoft.CodeAnalysis;

namespace TypeMessageGenerator
{
    [Generator]
    public class TypeMembersSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);

            var path = "C:\\MyFile\\abc\\mainMethod.txt";
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

        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}
