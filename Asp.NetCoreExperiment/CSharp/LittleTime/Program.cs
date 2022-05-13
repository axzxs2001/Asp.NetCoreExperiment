namespace LittleTime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {                
                var time = DateTime.Now;
                Console.WriteLine(time);
                Console.WriteLine(time.Kind);
                Console.WriteLine(time.Year);
                Console.WriteLine(time.Month);
                Console.WriteLine(time.Day);
                Console.WriteLine(time.Hour);
                Console.WriteLine(time.Minute);
                Console.WriteLine(time.Second);
                Console.WriteLine(time.Millisecond);
                Console.WriteLine(time.Microsecond);
                Console.WriteLine(time.Nanosecond);
                Console.WriteLine(time.Ticks);
                Console.ReadLine();
            }
        }
    }
}