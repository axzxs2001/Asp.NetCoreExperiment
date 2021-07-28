using System;

namespace StringComparison
{
    class Program
    {
        static void Main(string[] args)
        {
            
            for (var i = 0; i < 128; i++)
            {
                var str1 = ((char)i).ToString();
                var str2 = $"---------------{str1}---------------";
                var no1 = str2.IndexOf(str1);
                var no2 = str2.IndexOf(str1, System.StringComparison.Ordinal);

                if (no1 != no2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"不等  i={i},str1={str1},no1={no1},no2={no2}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"相等  i={i},str1={str1},no1={no1},no2={no2}");
                }
                Console.ResetColor();
            }
            //var c ="\n";
            //var s = $"a\r\na";
            //Console.WriteLine(s);

            //int idx = s.IndexOf(c);
            //Console.WriteLine("none:" + idx);

            //idx = s.IndexOf(c, System.StringComparison.Ordinal);
            //Console.WriteLine("Ordinal:" + idx);

            //idx = s.IndexOf(c, System.StringComparison.CurrentCulture);
            //Console.WriteLine("CurrentCulture:" + idx);

            //idx = s.IndexOf(c, System.StringComparison.InvariantCulture);
            //Console.WriteLine("InvariantCulture:" + idx);
            //Console.WriteLine(s.Replace("\n", "a"));
            //Console.WriteLine(s.Replace("\n", "a", System.StringComparison.Ordinal));
            //Console.WriteLine(s.Replace("\n", "a", System.StringComparison.CurrentCulture));
        }
    }
}
