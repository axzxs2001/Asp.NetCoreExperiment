
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections;
using System.Runtime.CompilerServices;

BenchmarkRunner.Run<Demo1>();

public class Demo1
{
    Season0 season0;
    Season1 season1;
    [GlobalSetup]
    public void Setup()
    {
        season0 = new Season0();
        season0[0] = "春";
        season0[1] = "夏";
        season0[2] = "秋";
        season0[3] = "冬";

        season1 = new Season1();
        season1[0] = "春";
        season1[1] = "夏";
        season1[2] = "秋";
        season1[3] = "冬";
    }


    [Benchmark()]
    public void P0()
    {
        foreach (var s in season0)
        {
            var s0 = s;
            //Console.WriteLine(s);
        }
    }

    [Benchmark]
    public void P1()
    {
        foreach (var s in season1)
        {
            var s0 = s;
            //Console.WriteLine(s);
        }
    }
}

[InlineArray(4)]
public struct Season0
{
    private string _name;
}

public struct Season1 : IEnumerable
{
    readonly string[] _arr;
    public Season1()
    {
        _arr = new string[4];
    }
    public string this[int index]
    {
        get => _arr[index];
        set => _arr[index] = value;
    }
    public IEnumerator GetEnumerator()
    {
        for (var i = 0; i < _arr.Length; i++)
        {
            yield return _arr[i];
        }
    }
}



//using System.ComponentModel;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

////var pr_1 = new PersonRecord("first", "last");
////Console.WriteLine($"FirstName:{pr_1.FirstName}");
////Console.WriteLine($"LastName:{pr_1.LastName}");
////Console.WriteLine($"Name:{pr_1.Name}");

////下面的写法是错误的
////var pr_2 = new PersonRecord();
////Console.WriteLine($"FirstName:{pr_2.FirstName}");
////Console.WriteLine($"LastName:{pr_2.LastName}");
////Console.WriteLine($"Name:{pr_2.Name}");

////var pc_1 = new PersonClass("first", "last");
////Console.WriteLine($"FirstName:{pc_1.FirstName}");
////Console.WriteLine($"LastName:{pc_1.LastName}");
////Console.WriteLine($"Name:{pc_1.Name}");
////pc_1.SetFirstName();
////Console.WriteLine($"Name:{pc_1.Name}");



////var pc_2 = new PersonClass();
////Console.WriteLine($"FirstName:{pc_2.FirstName}");
////Console.WriteLine($"LastName:{pc_2.LastName}");
////Console.WriteLine($"Name:{pc_2.Name}");


//var ps_1 = new PersonStruct("first", "last");
//Console.WriteLine($"FirstName:{ps_1.FirstName}");
//Console.WriteLine($"LastName:{ps_1.LastName}");
//Console.WriteLine($"Name:{ps_1.Name}");

//var ps_2 = new PersonStruct();
//Console.WriteLine($"FirstName:{ps_2.FirstName}");
//Console.WriteLine($"LastName:{ps_2.LastName}");
//Console.WriteLine($"Name:{ps_2.Name}");

////var prs_1 = new PersonRecordStruct("first", "last");
////Console.WriteLine($"FirstName:{prs_1.FirstName}");
////Console.WriteLine($"LastName:{prs_1.LastName}");
////Console.WriteLine($"Name:{prs_1.Name}");

////var prs_2 = new PersonRecordStruct();
////Console.WriteLine($"FirstName:{prs_2.FirstName}");
////Console.WriteLine($"LastName:{prs_2.LastName}");
////Console.WriteLine($"Name:{prs_2.Name}");


//public record PersonRecord(string FirstName, string LastName)
//{
//    public string Name => $"{FirstName} {LastName}";
//}

//public class PersonClass(string firstName, string LastName)
//{
//    public PersonClass() : this("", "")
//    {
//    }
//    public string FirstName { get; } = firstName;
//    public string LastName { get; } = LastName;
//    public string Name => $"{FirstName} {LastName}";
//    public void SetFirstName()
//    {
//        Console.WriteLine(firstName);
//    }
//}


//public struct PersonStruct(string firstName, string LastName)
//{
//    //public PersonStruct() : this("", "")
//    //{
//    //}
//    public string FirstName { get; } = firstName;
//    public string LastName { get; } = LastName;
//    public string Name => $"{FirstName} {LastName}";
//}
//public record struct PersonRecordStruct(string FirstName, string LastName)
//{
//    public string Name => $"{FirstName} {LastName}";
//}



////internal class NameOf
////{
////    public string S { get; } = "";
////    public static int StaticField;
////    public string NameOfLength { get; } = nameof(S.Length);
////    public static void NameOfExamples()
////    {
////        Console.WriteLine(nameof(S.Length));
////        Console.WriteLine(nameof(StaticField.MinValue));
////    }
////    [Description($"String {nameof(S.Length)}")]
////    public int StringLength(string s)
////    { return s.Length; }
////}

