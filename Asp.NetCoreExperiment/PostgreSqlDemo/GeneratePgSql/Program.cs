using System;
using Dapper;
using Npgsql;
using System.IO;
using System.Text;

namespace GeneratePgSql
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var con = new NpgsqlConnection("Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=RoseDB;"))
            {
                foreach (var table in con.Query<dynamic>(@"SELECT tablename ,cast(obj_description(relfilenode,'pg_class') as varchar) as comment
FROM pg_tables as tab
join pg_class as cla on tab.tablename=cla.relname
where schemaname='public';"))
                {
                    var sql = @$"select a.attnum,a.attname AS fieldname,t.typname as type,d.description from pg_class c, 
pg_attribute a , pg_type t, pg_description d
where c.relname = '{table.tablename}' and a.attnum>0 and a.attrelid = c.oid and a.atttypid = t.oid and d.objoid=a.attrelid and d.objsubid=a.attnum";
                    var file = $"{Directory.GetCurrentDirectory()}/{GetName(table.tablename)}.cs";
                    var code = new StringBuilder();
                    code.AppendLine("using System;");
                    code.AppendLine();
                    code.AppendLine("namespace GeneratePgSql");
                    code.AppendLine("{");
                    code.AppendLine(@$"    /// <summary>
    /// {table.comment}
    /// </summary>
    public class {GetName(table.tablename)}");
                    code.AppendLine("    {");
                    foreach (var item in con.Query<dynamic>(sql))
                    {
                        var type = "";
                        switch (item.type)
                        {
                            case "varchar":
                            case "text":
                                type = "string";
                                break;
                            case "timetz":
                                type = "DateTimeOffset";
                                break;
                            case "int4":
                                type = "int";
                                break;
                            case "int2":
                                type = "short";
                                break;
                            case "int8":
                                type = "long";
                                break;
                            case "numeric":
                                type = "double";
                                break;
                            case "bool":
                                type = "bool";
                                break;
                            case "date":
                                type = "DateTime";
                                break;
                        }
                        var codeString = @$"        /// <summary>
        /// {item.description}
        /// </summary>
        {type} {item.fieldname};
        /// <summary>
        /// {item.description}
        /// </summary>
        public {type} {GetName(item.fieldname)}
        {{
            get
            {{
                return {item.fieldname};
            }}
            set
            {{
                {item.fieldname}=value;
            }}
        }}
";
                        code.AppendLine(codeString);

                    }
                    code.AppendLine("    }");
                    code.AppendLine("}");

                    File.WriteAllText(file, code.ToString());
                }
            }
        }


        static string GetName(string name)
        {
            var words = name.Split('_');
            var handleName = new StringBuilder();
            foreach (var word in words)
            {
                handleName.Append($"{word.ToLower().Substring(0, 1).ToUpper()}{word.ToLower().Substring(1)}");
            }
            return handleName.ToString();
        }

    }
}

