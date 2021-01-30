using GreenDonut;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using HotChocolate.Fetching;
using HotChocolate.Language;
using HotChocolate.Types;
using Microsoft.Extensions.DiagnosticAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using System.Threading;
using System.Threading.Tasks;

namespace GraphQLDemo005
{

    class Program
    {
        static void Main(string[] args)
        {
            InterfaceDemo.Run();
        }
    }
    public class InterfaceDemo
    {

        public static void Run()
        {
            var schema = SchemaBuilder.New()
                .AddQueryType<Query>()
                //.AddType<AFormatType>()
                //.AddType<BFormatType>()
                .AddType<AFormat>()             
                .AddType<BFormat>()                
                .AddProjections()
                .Create();


            var executor = schema.MakeExecutable();
            Console.WriteLine(executor.Execute(@"
{
    formats
    {
        __typename,
        name,        
        ... on AFormat{
            a1,
            a2
        },
        ... on BFormat{
            b1,
            b2
        }
    } 
}").ToJson());

        }
    }

    #region 抽象类1 通不过
    public class Query
    {
        public Format[] GetFormats()
        {
            return new Format[]
            {
                    new AFormat{
                         Name="A",
                         A1="A1",
                         A2="A2"
                    },
                    new BFormat{
                       Name="B",
                       B1="B1",
                       B2="B2"
                    }
            };
        }
    }
    [InterfaceType]
    public abstract class Format
    {
        public string Name { get; set; }
    }
    public class AFormat : Format
    {
        public string A1 { get; set; }
        public string A2 { get; set; }
    }

    public class BFormat : Format
    {
        public string B1 { get; set; }
        public string B2 { get; set; }
    }
    #endregion
    #region 抽象类2

    //public class Query
    //{
    //    public Format[] GetFormats()
    //    {
    //        return new Format[]
    //        {
    //                new AFormat{
    //                     Name="A",
    //                     A1="A1",
    //                     A2="A2"
    //                },
    //                new BFormat{
    //                   Name="B",
    //                   B1="B1",
    //                   B2="B2"
    //                }
    //        };
    //    }
    //}

    //public abstract class Format
    //{
    //    public abstract string Name { get; set; }
    //}
    //public class AFormat : Format
    //{
    //    public override string Name { get; set; }
    //    public string A1 { get; set; }
    //    public string A2 { get; set; }
    //}

    //public class BFormat : Format
    //{
    //    public override string Name { get; set; }
    //    public string B1 { get; set; }
    //    public string B2 { get; set; }
    //}



    //public class FormatType : InterfaceType<Format>
    //{
    //    protected override void Configure(IInterfaceTypeDescriptor<Format> descriptor)
    //    {
    //        base.Configure(descriptor);
    //    }
    //}
    //public class AFormatType : ObjectType<AFormat>
    //{
    //    protected override void Configure(IObjectTypeDescriptor<AFormat> descriptor)
    //    {
    //        descriptor.Implements<FormatType>();
    //    }
    //}

    //public class BFormatType : ObjectType<BFormat>
    //{
    //    protected override void Configure(IObjectTypeDescriptor<BFormat> descriptor)
    //    {
    //        descriptor.Implements<FormatType>();
    //    }
    //}

    #endregion
    #region 接口
    //public class Query
    //{
    //    public IFormat[] GetFormats()
    //    {
    //        return new IFormat[]
    //        {
    //                new AFormat{
    //                     Name="A",
    //                     A1="A1",
    //                     A2="A2"
    //                },
    //                new BFormat{
    //                   Name="B",
    //                   B1="B1",
    //                   B2="B2"
    //                }
    //        };
    //    }
    //}

    //[GraphQLName("Format")]
    //public interface IFormat
    //{
    //    string Name { get; set; }

    //}

    //public class AFormat : IFormat
    //{
    //    public string Name { get; set; }

    //    public string A1 { get; set; }
    //    public string A2 { get; set; }
    //}

    //public class BFormat : IFormat
    //{
    //    public string Name { get; set; }

    //    public string B1 { get; set; }
    //    public string B2 { get; set; }
    //}
    #endregion
}



