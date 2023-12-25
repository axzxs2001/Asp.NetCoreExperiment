// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

Console.WriteLine(MyClass.int_add(1, 2));
Console.WriteLine(MyClass.string_add("abcd", "efg"));

var resultArray = new nint[6];
var resultPtr = MyClass.get_strings();

//for (int i = 0; i < 3; i++)
//{
//    var value = Marshal.ReadIntPtr(resultPtr + i);
//    Console.WriteLine(Marshal.PtrToStringBSTR(value));
//}
//Marshal.Copy(resultPtr, resultArray, 0, resultArray.Length);
//foreach (var i in resultArray)
//{
//    Console.WriteLine(Marshal.PtrToStringUTF8(i));
//}

public class MyClass
{
    // 定义非托管函数的签名
    [DllImport("classlib.dll")] // 指定DLL文件名
    public static extern int int_add(int a, int b);

    [DllImport("classlib.dll")] // 指定DLL文件名
    public static extern string string_add(string a, string b);


    [DllImport("classlib.dll")] // 指定DLL文件名
    public static extern IntPtr get_strings();
}