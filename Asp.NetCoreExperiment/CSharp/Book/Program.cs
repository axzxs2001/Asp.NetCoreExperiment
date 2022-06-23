namespace Book
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ArraryDemo01();
            ArrayDemo02();
        }

        static void ArraryDemo01()
        {
            //#引用类型，不管元素是什么类型
            var arr1 = new string[] { "a", "b", "c" };

            string[] arr2 = { "a", "b", "c", "d", "e", "f", "g", "h", "i" };

            var arr3 = new string[3];
            arr3[0] = "a";
            arr3[1] = "b";
            arr3[2] = "c";

            //#定长，不可变长

            for (var i = 0; i < arr2.Length; i++)
            {
                Console.WriteLine(arr2[i]);
            }
            foreach (var e in arr2)
            {
                Console.WriteLine(e);
            }



            //arr2元素和下标对应关系
            Console.WriteLine("---------原始数组----------");
            for (var i = 0; i < arr2.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{i}");
                Console.ResetColor();
                Console.Write(":" + arr2[i] + (i != arr2.Length - 1 ? "," : ""));
            }
            Console.WriteLine();
            Console.WriteLine("-------------------");
            //下标为2的元素
            Console.WriteLine("下标为2的元素：" + arr2[2]);


            //5以后的元素 arr2[5..]
            Console.WriteLine("5以后的元素：" + string.Join(',', arr2[5..]));
            //另一种写法
            var r1 = 5..;
            Console.WriteLine("另一种写法,5以后的元素：" + string.Join(',', arr2[r1]));

            //4以前的元素 arr2[..4]
            Console.WriteLine("4以前的元素：" + string.Join(',', arr2[..4]));
            //另一种写法
            var r2 = ..4;
            Console.WriteLine("另一种写法,4以前的元素：" + string.Join(',', arr2[r2]));

            //从下标从2到4开始的元素，不包含下标4 arr2[2..4]
            Console.WriteLine("2~4的元素：" + string.Join(',', arr2[2..4]));
            //另一种写法
            var r3 = 2..4;
            Console.WriteLine("另一种写法,2~4的元素：" + string.Join(',', arr2[r3]));



            //有^情况下，不能是0，从1开始 arr2[^1]
            Console.WriteLine("从后第2个元素：" + arr2[^2]);
            //另一种写法
            var r4 = ^2;
            Console.WriteLine("另一种写法,从后第2个元素：" + arr2[r4]);


            //从后倒数第5个以后的元素 arr2[^5..]
            Console.WriteLine("倒数第5个以后的元素：" + string.Join(',', arr2[^5..]));
            //另一种写法
            var r5 = ^5..;
            Console.WriteLine("另一种写法,倒数第5个以后的元素：" + string.Join(',', arr2[r5]));
            //从后倒数第5个到倒数第2个元素 arr2[^5..^2]
            Console.WriteLine("倒数第5个到倒数第2个元素：" + string.Join(',', arr2[^5..^2]));
            //另一种写法
            var r6 = ^5..^2;
            Console.WriteLine("另一种写法,倒数第5个到倒数第2个元素：" + string.Join(',', arr2[r6]));


            var array = Array.CreateInstance(typeof(string), 3);
            array.SetValue("a", 0);
            array.SetValue("b", 1);
            array.SetValue("c", 2);
            Console.WriteLine(array.GetValue(1));


            Array doubleArr = new double[] { 1.1, 1.2, 1.3, 1.4 };
            Console.WriteLine(string.Join(',', (doubleArr as double[])!));

        }

        static void ArrayDemo02()
        {
            //┼○●  ╋
            var checkerboard = new string[30, 30];
            for (var r = 0; r < checkerboard.GetLength(0); r++)
            {
                for (var c = 0; c < checkerboard.GetLength(1); c++)
                {
                    checkerboard[r, c] = "┼";
                }
            }
            for (var r = 0; r < checkerboard.GetLength(0); r++)
            {
                for (var c = 0; c < checkerboard.GetLength(1); c++)
                {
                    Console.Write(checkerboard[r, c]);
                }
                Console.WriteLine();
            }
            Console.ReadLine();
            Console.Clear();
            checkerboard[15, 15] = "●";
            checkerboard[15, 14] = "○";
            for (var r = 0; r < checkerboard.GetLength(0); r++)
            {
                for (var c = 0; c < checkerboard.GetLength(1); c++)
                {
                    Console.Write(checkerboard[r, c]);
                }
                Console.WriteLine();
            }
        }

        static void ArrayDemo3()
        {
            var array = new float[3][];
            array[0] = new float[3] { 1.1f, 1.2f, 1.3f };
            array[1] = new float[4] { 2.1f, 2.2f, 2.3f, 2.4f }; ;
            array[2] = new float[6] { 3.1f, 3.2f, 3.3f, 3.4f, 3.5f, 3.6f };
            foreach (var arr in array)
            {
                foreach (var c in arr)
                {
                    Console.WriteLine(c);
                }
            }
        }
    }
}