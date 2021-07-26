using System;

namespace StringComparison
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = "\n";
            var s = "Hello\r\nworld!";
            int idx = s.IndexOf(c);
            Console.WriteLine("none:" + idx);

            idx = s.IndexOf(c, System.StringComparison.Ordinal);
            Console.WriteLine("Ordinal:" + idx);

            idx = s.IndexOf(c, System.StringComparison.CurrentCulture);
            Console.WriteLine("CurrentCulture:" + idx);

            idx = s.IndexOf(c, System.StringComparison.InvariantCulture);
            Console.WriteLine("InvariantCulture:" + idx);




            c = "Hello";

            if (s.StartsWith(c)) 
            {
                Console.WriteLine("Hello是存在的1");
            }
            if (s.StartsWith(c,System.StringComparison.Ordinal))
            {
                Console.WriteLine("Hello是存在的2");
            }


            ReadOnlySpan<char> span = s.AsSpan();
            if (span.StartsWith(c))
            {
                Console.WriteLine("Hello是存在的3");
            }
          
            if (span.StartsWith(c,System.StringComparison.Ordinal))         
            {
                Console.WriteLine("Hello是存在的4");
            }
        }
    }
}
