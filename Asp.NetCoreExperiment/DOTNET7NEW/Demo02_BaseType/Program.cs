using System.ComponentModel;
using System.Numerics;


var i16 = Int16.MaxValue;
Console.WriteLine($"Int16：{i16}");
var i32 = Int32.MaxValue;
Console.WriteLine($"Int32：{i32}");
var i64 = Int64.MaxValue;
Console.WriteLine($"Int64：{i64}");
var i128 = Int128.MaxValue;
Console.WriteLine($"Int128：{i128}");

Int128Converter ic = new Int128Converter();
var i2 = (Int128?)ic.ConvertFrom(i128.ToString());
Console.WriteLine(i2);


//Console.WriteLine(Int128.MaxValue);
//BigInteger bi = Int128.MaxValue + 1;
//Console.WriteLine(bi);
//bi = Int128.MaxValue;
//Console.WriteLine(bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi * bi);



Half h1 = (Half)0.123456789;
float h2 = 0.123456789f;
double h3 = 0.123456789012345678d;
Console.WriteLine(h1);
Console.WriteLine(h2);
Console.WriteLine(h3);

var aaa = new Int128Converter();


DateOnly dateOnly = DateOnly.Parse("2022-11-01");
TimeOnly timeOnly = TimeOnly.FromDateTime(DateTime.Now);
