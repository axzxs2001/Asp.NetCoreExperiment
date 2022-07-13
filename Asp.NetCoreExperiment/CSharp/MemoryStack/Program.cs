namespace MemoryStack
{
    unsafe internal class Program
    {
        static void Main(string[] args)
        {
            int i1 = 10;
            int i2 = 20;
            Console.WriteLine("Main i1 " + (long)&i1);
            Console.WriteLine("Main i2 " + (long)&i2);
            Console.WriteLine("Main i2-i1 " + ((long)&i2 - (long)&i1));

            var i3 = Add(i1, i2);
            Console.WriteLine("Main i3 " + (long)&i3);
            Console.WriteLine("Main i3-i2 " + ((long)&i3 - (long)&i2));
        }

        static int Add(int i1, int i2)
        {
            Console.WriteLine("Add i1 " + (long)&i1);
            Console.WriteLine("Add i2 " + (long)&i2);
            Console.WriteLine("Add i2-i1 " + ((long)&i2 - (long)&i1));
            var i3 = i1 + i2;
            Console.WriteLine("Add i3 " + (long)&i3);
            Console.WriteLine("Add i3-i2 " + ((long)&i3 - (long)&i2));
            return i3;
        }
    }
}