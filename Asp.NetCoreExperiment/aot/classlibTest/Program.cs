// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

Console.WriteLine(MyClass.int_add(1, 2));

Console.WriteLine(MyClass.string_add("abcd", "efg"));

var resultPtr = MyClass.get_doubles();
var Items = new double[2];
for (int j = 0; j < 2; j++)
{
    Items[j] = Marshal.PtrToStructure<double>(resultPtr + j * Marshal.SizeOf<double>());
    Console.WriteLine(Items[j]);
}

var sum = MyClass.set_doubles(new double[] { 5.6, 6.7 }, 2);
Console.WriteLine(sum);


Console.ReadLine();


public class MyClass
{
    // 定义非托管函数的签名
    [DllImport("classlib.dll")] // 指定DLL文件名
    public static extern int int_add(int a, int b);

    [DllImport("classlib.dll")] // 指定DLL文件名
    public static extern string string_add(string a, string b);


    [DllImport("classlib.dll")] // 指定DLL文件名
    public static extern IntPtr get_doubles();

    [DllImport("classlib.dll")] // 指定DLL文件名
    public static extern double set_doubles(double[] arr, int length);
}