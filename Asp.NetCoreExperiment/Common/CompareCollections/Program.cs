using System;
using System.Collections.Generic;
using System.Linq;

namespace CompareCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            var leftKes = new List<string>() { "1", "2", "3","4" };
            var rightKes = new List<string>() { "3", "4","5" ,"6"};

            Console.WriteLine("Left集合");
            Console.WriteLine(string.Join(',', leftKes));
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("Right集合");
            Console.WriteLine(string.Join(',', rightKes));
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("Left多的 ");
            Console.WriteLine(string.Join(',', leftKes.Except(rightKes)));
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("Right多的 ");
            Console.WriteLine(string.Join(',', rightKes.Except(leftKes).ToList()));
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("Left和Right交集 ");
            Console.WriteLine(string.Join(',', rightKes.Intersect(leftKes)));
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("Left和Right并集 ");
            Console.WriteLine(string.Join(',', leftKes.Union(rightKes)));       
            Console.WriteLine("-------------------------------------");
            
            Console.WriteLine("Left和Right补集 ");       
            Console.WriteLine(string.Join(',', leftKes.Union(rightKes).Except(rightKes.Intersect(leftKes))));
            Console.WriteLine("-------------------------------------");

        }
    }
}
