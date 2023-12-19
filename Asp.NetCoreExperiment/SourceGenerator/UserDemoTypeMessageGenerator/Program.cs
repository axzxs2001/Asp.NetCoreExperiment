using System.Reflection;

var assembly = Assembly.GetExecutingAssembly();
var stream = assembly.GetManifestResourceStream("UserDemoTypeMessageGenerator.type.txt");
var reader = new StreamReader(stream!);
Console.WriteLine(reader.ReadToEnd());


Console.ReadLine();