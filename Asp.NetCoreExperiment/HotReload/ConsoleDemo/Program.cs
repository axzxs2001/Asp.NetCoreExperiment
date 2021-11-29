

Console.WriteLine("回车继续：");
Console.ReadLine();
File.AppendAllLines(@"C:\NetStars\GSWA.txt", new string[] { $"0{ DateTime.Now.ToString()}" });

Console.ReadLine();
File.AppendAllLines(@"C:\NetStars\GSWA.txt", new string[] { $"1-{ DateTime.Now.ToString()}" });

