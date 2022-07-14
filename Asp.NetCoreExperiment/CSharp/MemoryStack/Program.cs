using System.Runtime.InteropServices;

namespace MemoryStack
{
    unsafe internal class Program
    {
        static void Main(string[] args)
        {
            //TestDouble();
            //TestString();
            //Console.WriteLine("------------------");
            //TestString2();
            //TestTestClass();
            //TestTestStruct();

            var v1 = "aa";
            var v2 = "aa" + v1;
            var h1 = GCHandle.Alloc(v2, GCHandleType.Pinned);
            Console.WriteLine("TestString2 v1对象地址= " + (long)h1.AddrOfPinnedObject());

            var v3 = "aa";
            var v4 = "aa" + v3;
            var h2 = GCHandle.Alloc(v4, GCHandleType.Pinned);
            Console.WriteLine("TestString2 v1对象地址= " + (long)h2.AddrOfPinnedObject());
            AA();
        }
        static void AA()
        {
            var v3 = "aa";
            var v4 = "aa" + v3;
            var h2 = GCHandle.Alloc(v4, GCHandleType.Pinned);
            Console.WriteLine("TestString2 v1对象地址= " + (long)h2.AddrOfPinnedObject());
        }

        static void TestTestStruct()
        {
            var v1 = new TestStruct();
            Console.WriteLine("TestStruct原v1对象地址= " + (long)&v1);
            var v2 = new TestStruct();
            Console.WriteLine("TestStruct原v2对象地址= " + (long)&v2);
            Console.WriteLine("TestStruct v2-v1 " + ((long)&v2 - (long)&v1));

            var v3 = Add(v1, v2);
            Console.WriteLine("TestStruct原v3对象地址= " + (long)&v3);
            Console.WriteLine("TestStruct v3-v2 " + ((long)&v3 - (long)&v2));

        }
        static TestStruct Add(TestStruct v1, TestStruct v2)
        {
            Console.WriteLine("Add TestStruct v1对象地址= " + (long)&v1);
            Console.WriteLine("Add TestStruct v2对象地址= " + (long)&v2);
            Console.WriteLine("Add TestStruct  v2-v1 " + ((long)&v2 - (long)&v1));
            var v3 = new TestStruct();
            v3.i = v1.i + v2.i;
            Console.WriteLine("Add TestStruct v3对象地址" + (long)&v3);
            Console.WriteLine("Add TestStruct  v3-v2 " + ((long)&v3 - (long)&v2));
            return v3;
        }


        static void TestTestClass()
        {
            var v1 = new TestClass();
            var h1 = GCHandle.Alloc(v1, GCHandleType.Pinned);
            Console.WriteLine("TestTestClass v1对象地址= " + (long)h1.AddrOfPinnedObject());
            var v2 = new TestClass();
            var h2 = GCHandle.Alloc(v2, GCHandleType.Pinned);
            Console.WriteLine("TestTestClass v2对象地址= " + (long)h2.AddrOfPinnedObject());

            Console.WriteLine("TestTestClass v2-v1 " + ((long)h2.AddrOfPinnedObject() - (long)h1.AddrOfPinnedObject()));

            var v3 = Add(v1, v2);
            var h3 = GCHandle.Alloc(v3, GCHandleType.Pinned);
            Console.WriteLine("TestTestClass 3对象地址= " + (long)h3.AddrOfPinnedObject());
            Console.WriteLine("TestTestClass v3-v2 " + ((long)h3.AddrOfPinnedObject() - (long)h2.AddrOfPinnedObject()));
          

        }
        static TestClass Add(TestClass v1, TestClass v2)
        {
            var h1 = GCHandle.Alloc(v1, GCHandleType.Pinned);
            Console.WriteLine("Add中的v1对象地址= " + (long)h1.AddrOfPinnedObject());
            var h2 = GCHandle.Alloc(v2, GCHandleType.Pinned);
            Console.WriteLine("Add中的v2对象地址= " + (long)h2.AddrOfPinnedObject());

            Console.WriteLine("Add中 v2-v1 " + ((long)h2.AddrOfPinnedObject() - (long)h1.AddrOfPinnedObject()));

            var v3 = new TestClass();
            v3.i = v1.i + v2.i;
            var h3 = GCHandle.Alloc(v3, GCHandleType.Pinned);
            Console.WriteLine("Add中的v3对象地址= " + (long)h3.AddrOfPinnedObject());
            Console.WriteLine("Add中 v3-v2 " + ((long)h3.AddrOfPinnedObject() - (long)h2.AddrOfPinnedObject()));
            Console.WriteLine("Add中 v3-v1 " + ((long)h3.AddrOfPinnedObject() - (long)h1.AddrOfPinnedObject()));
            return v3;
        }
        static void TestString2()
        {
            var v1 = "aaaa";
            var v2 = "bbbb";
            var h1 = GCHandle.Alloc(v1, GCHandleType.Pinned);
            Console.WriteLine("TestString2 v1对象地址= " + (long)h1.AddrOfPinnedObject());
            var h2 = GCHandle.Alloc(v2, GCHandleType.Pinned);
            Console.WriteLine("TestString2 v2对象地址= " + (long)h2.AddrOfPinnedObject());
            Console.WriteLine("TestString2 v2-v1 " + ((long)h2.AddrOfPinnedObject() - (long)h1.AddrOfPinnedObject()));
            var v3 = Add2(v1, v2);
            var h3 = GCHandle.Alloc(v3, GCHandleType.Pinned);
            Console.WriteLine("TestString2 v3对象地址= " + (long)h3.AddrOfPinnedObject());
            Console.WriteLine("TestString2 v3-v2 " + ((long)h3.AddrOfPinnedObject() - (long)h2.AddrOfPinnedObject()));
  

        }
        static string Add2(string v1, string v2)
        {
            var h1 = GCHandle.Alloc(v1, GCHandleType.Pinned);
            Console.WriteLine("Add2中的v1对象地址= " + (long)h1.AddrOfPinnedObject());
            var h2 = GCHandle.Alloc(v2, GCHandleType.Pinned);
            Console.WriteLine("Add2中的v2对象地址= " + (long)h2.AddrOfPinnedObject());
            Console.WriteLine("Add2 v2-v1 " + ((long)h2.AddrOfPinnedObject() - (long)h1.AddrOfPinnedObject()));
            var v3 = v1 + v2;
            var h3 = GCHandle.Alloc(v3, GCHandleType.Pinned);
            Console.WriteLine("Add2中的v3对象地址= " + (long)h3.AddrOfPinnedObject());
            Console.WriteLine("Add2 v3-v2 " + ((long)h3.AddrOfPinnedObject() - (long)h2.AddrOfPinnedObject()));
            Console.WriteLine("Add2 v3-v1 " + ((long)h3.AddrOfPinnedObject() - (long)h1.AddrOfPinnedObject()));
            return v3;
        }

        static void TestString()
        {
            long ad1, ad2, ad3;
            var v1 = "aaaa";
            var v2 = "bbbb";
            fixed (char* p = v1)
            {
                ad1 = (long)p;
                Console.WriteLine("TestString v1字符串地址= " + (long)p);
            }
            fixed (char* p = v2)
            {
                ad2 = (long)p;
                Console.WriteLine("TestString v2字符串地址= " + (long)p);
            }
            Console.WriteLine("TestString v2-v1 " + (ad2 - ad1));
            var v3 = Add(v1, v2);
            fixed (char* p = v3)
            {
                ad3 = (long)p;
                Console.WriteLine("TestString v3字符串地址= " + (long)p);
            }
            Console.WriteLine("TestString v3-v2 " + (ad3 - ad2));
        }

        static string Add(string v1, string v2)
        {
            long ad1, ad2, ad3;
            fixed (char* p = v1)
            {
                ad1 = (long)p;
                Console.WriteLine("Add中v1字符串地址= " + (long)p);
            }
            fixed (char* p = v2)
            {
                ad2 = (long)p;
                Console.WriteLine("Add中v2字符串地址= " + (long)p);
            }
            Console.WriteLine("Add中 v2-v1 " + (ad2 - ad1));
            var v3 = v1 + v2;
            fixed (char* p = v3)
            {
                ad3 = (long)p;
                Console.WriteLine("Add中v3字符串地址= " + (long)p);
            }
            Console.WriteLine("Add中 v3-v2 " + (ad3 - ad2));
            Console.WriteLine("Add中 v3-v1 " + (ad3 - ad1));
            return v3;
        }
        static void TestDouble()
        {
            var v1 = 1.00001d;
            var v2 = 2.00002d;
            Console.WriteLine("TestDouble v1 " + (long)&v1);
            Console.WriteLine("TestDouble v2 " + (long)&v2);
            Console.WriteLine("TestDouble v2-v1 " + ((long)&v2 - (long)&v1));
            var v3 = Add(v1, v2);
            Console.WriteLine("TestDouble v3 " + (long)&v3);
            Console.WriteLine("TestDouble v3-v2 " + ((long)&v3 - (long)&v2));
            Console.WriteLine("TestDouble v3-v1 " + ((long)&v3 - (long)&v1));
        }

        static double Add(double v1, double v2)
        {
            Console.WriteLine("Add v1 " + (long)&v1);
            Console.WriteLine("Add v2 " + (long)&v2);
            Console.WriteLine("Add v2-v1 " + ((long)&v2 - (long)&v1));
            var v3 = v1 + v2;
            Console.WriteLine("Add v3 " + (long)&v3);
            Console.WriteLine("Add v3-v2 " + ((long)&v3 - (long)&v2));
            Console.WriteLine("Add v3-v1 " + ((long)&v3 - (long)&v1));
            return v3;
        }
        //static void TestDecmal()
        //{
        //    var v1 = 1.00001m;
        //    var v2 = 2.00002m;
        //    Console.WriteLine("TestDecmal v1 " + (long)&v1);
        //    Console.WriteLine("TestDecmal v2 " + (long)&v2);
        //    Console.WriteLine("TestDecmal v2-v1 " + ((long)&v2 - (long)&v1));

        //    var v3 = Add(v1, v2);
        //    Console.WriteLine("TestDecmal v3 " + (long)&v3);
        //    Console.WriteLine("TestDecmal v3-v2 " + ((long)&v3 - (long)&v2));
        //    Console.WriteLine("TestDecmal v3-v1 " + ((long)&v3 - (long)&v1));
        //}

        //static decimal Add(decimal v1, decimal v2)
        //{
        //    Console.WriteLine("Add v1 " + (long)&v1);
        //    Console.WriteLine("Add v2 " + (long)&v2);
        //    Console.WriteLine("Add v2-v1 " + ((long)&v2 - (long)&v1));
        //    var v3 = v1 + v2;
        //    Console.WriteLine("Add v3 " + (long)&v3);
        //    Console.WriteLine("Add v3-v2 " + ((long)&v3 - (long)&v2));
        //    Console.WriteLine("Add v3-v1 " + ((long)&v3 - (long)&v1));
        //    return v3;
        //}
        //static void TestBool()
        //{
        //    var v1 = true;
        //    var v2 = false;
        //    Console.WriteLine("TestBool v1 " + (long)&v1);
        //    Console.WriteLine("TestBool v2 " + (long)&v2);
        //    Console.WriteLine("TestBool v2-v1 " + ((long)&v2 - (long)&v1));

        //    var v3 = Add(v1, v2);
        //    Console.WriteLine("TestBool v3 " + (long)&v3);
        //    Console.WriteLine("TestBool v3-v2 " + ((long)&v3 - (long)&v2));
        //    Console.WriteLine("TestBool v3-v1 " + ((long)&v3 - (long)&v1));
        //}

        //static bool Add(bool v1, bool v2)
        //{
        //    Console.WriteLine("Add v1 " + (long)&v1);
        //    Console.WriteLine("Add v2 " + (long)&v2);
        //    Console.WriteLine("Add v2-v1 " + ((long)&v2 - (long)&v1));
        //    var v3 = v1 && v2;
        //    Console.WriteLine("Add v3 " + (long)&v3);
        //    Console.WriteLine("Add v3-v2 " + ((long)&v3 - (long)&v2));
        //    Console.WriteLine("Add v3-v1 " + ((long)&v3 - (long)&v1));
        //    return v3;
        //}

        //static void TestInt()
        //{
        //    int v1 = 10;
        //    int v2 = 20;
        //    Console.WriteLine("TestInt v1 " + (long)&v1);
        //    Console.WriteLine("TestInt v2 " + (long)&v2);
        //    Console.WriteLine("TestInt v2-v1 " + ((long)&v2 - (long)&v1));

        //    var v3 = Add(v1, v2);
        //    Console.WriteLine("TestInt v3 " + (long)&v3);
        //    Console.WriteLine("TestInt v3-v2 " + ((long)&v3 - (long)&v2));
        //    Console.WriteLine("TestInt v3-v1 " + ((long)&v3 - (long)&v1));
        //}

        //static int Add(int v1, int v2)
        //{
        //    Console.WriteLine("Add v1 " + (long)&v1);
        //    Console.WriteLine("Add v2 " + (long)&v2);
        //    Console.WriteLine("Add v2-v1 " + ((long)&v2 - (long)&v1));
        //    var v3 = v1 + v2;
        //    Console.WriteLine("Add v3 " + (long)&v3);
        //    Console.WriteLine("Add v3-v2 " + ((long)&v3 - (long)&v2));
        //    Console.WriteLine("Add v3-v1 " + ((long)&v3 - (long)&v1));
        //    return v3;
        //}
    }


    class TestClass
    {
        public int i = 100;
    }

    struct TestStruct
    {
        public TestStruct()
        {
            i = 100;
        }
        public long i;
    }
}