using System;

namespace CSharpDemo02
{
    class Program
    {
        static void Main(string[] args)
        {
            //out
            var s = "123";
            if (int.TryParse(s, out var result))
            {
                Console.WriteLine(result);
            }
            //is
            object count = 5;
            if (count is int number)
                Console.WriteLine(number);
            else
                Console.WriteLine($"{count} is not an integer");

            //switch
            P p = new C2() { I = 101 };
            switch (p)
            {
                case C1 c1:
                    Console.WriteLine($"C1.I={c1.I}");
                    break;
                case C2 c2 when c2.I > 100:
                    Console.WriteLine($"C2.I={c2.I} > 100");
                    break;
                case C2 c2 when c2.I <= 100:
                    Console.WriteLine($"C2.I={c2.I} <= 100");
                    break;
                case C3 c3:
                    Console.WriteLine($"C3.I={c3.I}");
                    break;
            }

            var p1 = p switch
            {
                { I: 1 } => 100,
                _ => 0
            };


            //ref
            int[,] sourceMatrix = new int[10, 10];
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                    sourceMatrix[x, y] = x * 10 + y;

            //var indices = Find(sourceMatrix, (val) => val == 42);
            //Console.WriteLine(indices);
            //Console.WriteLine(sourceMatrix[indices.i, indices.j]);
            //sourceMatrix[indices.i, indices.j] = 24;
            //Console.WriteLine(indices);
            //Console.WriteLine(sourceMatrix[indices.i, indices.j]);

            ref var valItem = ref Find1(sourceMatrix, (val) => val == 42);
            Console.WriteLine($"valItem={valItem}");
            Console.WriteLine("-----赋值 valItem = 24");
            valItem = 24;
            Console.WriteLine($"sourceMatrix[4, 2]={sourceMatrix[4, 2]}");
            Console.WriteLine("-----赋值  sourceMatrix[4, 2] = 42");
            sourceMatrix[4, 2] = 42;
            Console.WriteLine($"sourceMatrix[4, 2]={sourceMatrix[4, 2]}");
            Console.WriteLine($"valItem={valItem}");


            //default
            int i = default;
            Console.WriteLine(i);
            string ss = default;
            Console.WriteLine(ss);
            Func<string, bool> whereClause = default;
            Console.WriteLine(whereClause);
        }
        static void ABC(in int i, in string s, int t)
        {
            Console.WriteLine($"ABC.i={i},ABC.s={s},ABC.t={t}");
            //i = 100;
            t = 100;
        }
        static (int i, int j) Find(int[,] matrix, Func<int, bool> predicate)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (predicate(matrix[i, j]))
                        return (i, j);
            return (-1, -1); // Not found
        }
        static ref int Find1(int[,] matrix, Func<int, bool> predicate)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (predicate(matrix[i, j]))
                        return ref matrix[i, j];
            throw new InvalidOperationException("Not found"); // Not found
        }
    }

    class P
    {
        public virtual int I { get; set; }
    }
    class C1 : P
    {
        public override int I { get; set; }
    }
    class C2 : P
    {
        public override int I { get; set; }
    }
    class C3 : P
    {
        public override int I { get; set; }
    }
}
