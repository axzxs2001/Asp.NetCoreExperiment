
using System.Globalization;
using UnitsNet;

var janpese = new CultureInfo("ja-JP");
var russian = new CultureInfo("ru-RU");
var chinese = new CultureInfo("zh-CN");
var oneKg = Mass.FromKilograms(1);

Thread.CurrentThread.CurrentCulture = russian;
Console.WriteLine(oneKg.ToString());
Thread.CurrentThread.CurrentCulture = chinese;
Console.WriteLine(oneKg.ToString());



