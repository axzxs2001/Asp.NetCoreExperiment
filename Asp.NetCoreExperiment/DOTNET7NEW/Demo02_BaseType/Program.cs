using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.InteropServices;


//var i16 = Int16.MaxValue;
//Console.WriteLine($"Int16：{i16}");
//var i32 = Int32.MaxValue;
//Console.WriteLine($"Int32：{i32}");
//var i64 = Int64.MaxValue;
//Console.WriteLine($"Int64：{i64}");
//var i128 = Int128.MaxValue;
//Console.WriteLine($"Int128：{i128}");

//Int128Converter ic = new Int128Converter();
//var i2 = (Int128?)ic.ConvertFrom(i128.ToString());
//Console.WriteLine(i2);


//Console.WriteLine(Int128.MaxValue);
//BigInteger bi = Int128.MaxValue + 1;
//Console.WriteLine(bi);
//bi = Int128.MaxValue;
//Console.WriteLine(bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi);
Console.WriteLine($"short max：{short.MaxValue}");
Console.WriteLine($"short min：{short.MinValue}");
short s = 123;
Console.WriteLine($"short值：{s}，占{Marshal.SizeOf(s)}字节");
Console.WriteLine("----------------------------------");

Console.WriteLine($"int max：{int.MaxValue}");
Console.WriteLine($"int min：{int.MinValue}");
int i = 123;
Console.WriteLine($"int值：{i}，占{Marshal.SizeOf(i)}字节");
Console.WriteLine("----------------------------------");

Console.WriteLine($"nint max：{nint.MaxValue}");
Console.WriteLine($"nint min：{nint.MinValue}");
nint n = 123;
Console.WriteLine($"nint值：{n}，占{Marshal.SizeOf(n)}字节");
Console.WriteLine("----------------------------------");

Console.WriteLine($"long max：{long.MaxValue}");
Console.WriteLine($"long min：{long.MinValue}");
long l = 123;
Console.WriteLine($"long值：{l}，占{Marshal.SizeOf(l)}字节");
Console.WriteLine("----------------------------------");

Console.WriteLine($"Int128 max：{Int128.MaxValue}");
Console.WriteLine($"Int128 min：{Int128.MinValue}");
Int128 i128 = 123;
Console.WriteLine($"Int128值：{i128}，占{Marshal.SizeOf(i128)}字节");
Console.WriteLine("----------------------------------");

Console.WriteLine($"Half max：{Half.MaxValue}");
Console.WriteLine($"Half min：{Half.MinValue}");
Half h = (Half)43210.123456789;
Console.WriteLine($"Half值：{h}，占{Marshal.SizeOf(h)}字节");
Console.WriteLine("----------------------------------");

Console.WriteLine($"float max：{float.MaxValue}");
Console.WriteLine($"float min：{float.MinValue}");
float f = 0.123456789f;
Console.WriteLine($"float值：{f}，占{Marshal.SizeOf(f)}字节");
Console.WriteLine("----------------------------------");

Console.WriteLine($"double max：{double.MaxValue}");
Console.WriteLine($"double min：{double.MinValue}");
double d = 0.123456789012345678d;
Console.WriteLine($"double值：{d}，占{Marshal.SizeOf(d)}字节");

Console.WriteLine("----------------------------------");

Console.WriteLine($"decimal max：{decimal.MaxValue}");
Console.WriteLine($"decimal min：{decimal.MinValue}");
var m = 0.12345678901234567890123456m;
Console.WriteLine($"decimal值：{m}，占{Marshal.SizeOf(m)}字节");
Console.WriteLine("----------------------------------");