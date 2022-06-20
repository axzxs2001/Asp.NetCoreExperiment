using Microsoft.Data.SqlClient;
using System.CommandLine;
using System.CommandLine.Binding;
using System.Data;
using Dapper;
using System.Text;

//创建根命令
var rootCommand = new RootCommand("这是一款C#开发辅助工具，CSRebot");
rootCommand.SetHandler(() =>
{
    Console.WriteLine("欢迎使用CSRebot");
});
//创建子命令 show
var dbtoCommand = new Command("dbto", "从数据库生成");

#region 选项 language
//创建子命令选项 language 别名 lan
var languageOption = new Option<string>(name: "--language", description: "从数据库生成实体类的语言")
{
    IsRequired = true,
}.FromAmong("c#", "C#", "csharp", "CSharp", "go", "GO", "java", "JAVA");
languageOption.AddAlias("-lan");
languageOption.AddAlias("-l");
//添加language选项到dbto命令中
dbtoCommand.AddOption(languageOption);
#endregion

#region connestionString
//创建子命令选项 connestionString 别名 constr
var connectionStringOption = new Option<string>(name: "--connectionstring", description: "数据库连接字符串")
{
    IsRequired = true,
};
connectionStringOption.AddAlias("-constr");

//添加language选项到dbto命令中
dbtoCommand.AddOption(connectionStringOption);
#endregion

#region dbtype
//创建子命令选项 connestionString 别名 constr
var dbTypeOption = new Option<string>(name: "--dbtype", description: "数据库类型")
{
    IsRequired = true,
}.FromAmong("mssql", "mysql", "pgsql"); ;
dbTypeOption.AddAlias("-dbt");
dbTypeOption.AddAlias("-t");
//添加language选项到dbto命令中
dbtoCommand.AddOption(dbTypeOption);
#endregion

//设置命令dbto执行的动作，这是带上language参数
dbtoCommand.SetHandler(async (string language, string connectionstring, string dbType) =>
{
    switch (language.ToLower())
    {
        case "c#":
        case "csharp":
            await DBToCSharpAsync(connectionstring, dbType);
            break;
        case "go":
            break;
        case "java":
            break;
        default:
            break;

    }
}, languageOption, connectionStringOption, dbTypeOption);

//添加命令dbto到 根命令中
rootCommand.Add(dbtoCommand);
await rootCommand.InvokeAsync(args);


static async Task DBToCSharpAsync(string connectionString, string dbType)
{
    switch (dbType.ToLower())
    {
        case "mssql":
            await MSSQLToCSharpAsync(connectionString);
            Console.WriteLine("mssql");
            break;

        default:
            break;
    }
}

static async Task MSSQLToCSharpAsync(string connectionString)
{
    using var con = new SqlConnection(connectionString);
    var tableNames = await con.QueryAsync<string>("select name from sysobjects where xtype='U'");
    var dbPath = $"{Environment.CurrentDirectory}\\{con.Database}";
    if (!Directory.Exists(dbPath))
    {
        Directory.CreateDirectory(dbPath);
    }
    foreach (var tableName in tableNames)
    {
        var tablefieldses = await con.QueryAsync<dynamic>("SELECT syscolumns.name,systypes.name as typename FROM syscolumns, systypes WHERE syscolumns.xusertype = systypes.xusertype AND syscolumns.id = object_id(@tablename)", new { tableName });
        var csBuilder = new StringBuilder();
        csBuilder.AppendLine($"public class {tableName}");
        csBuilder.AppendLine("{");
        foreach (var tablefields in tablefieldses)
        {
            var typeName = TypeMap.MSSQLToCSharp[tablefields.typename];
            var name = tablefields.name;
            csBuilder.AppendLine($"   public {typeName} {name}{{get;set;}}");
        }
        csBuilder.AppendLine("}");
        File.WriteAllText($"{dbPath}\\{tableName}.cs", csBuilder.ToString(), Encoding.UTF8);
    }
}

static class TypeMap
{
    internal static Dictionary<string, string> MSSQLToCSharp => new Dictionary<string, string>
    {
        {"int","int" },
        {"varchar","string" },
        {"nvarchar","string" },
        {"bit","bool" },
        {"bigint","long" },
    };

    internal static Dictionary<string, string> MySQLToCSharp => new Dictionary<string, string>
    {
        {"","" }
    };
    internal static Dictionary<string, string> PgSQLToCSharp => new Dictionary<string, string>
    {
        {"","" }
    };
}