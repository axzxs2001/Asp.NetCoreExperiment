using System;

namespace TestProject01
{
    public class TestClass
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public bool Get(int i)
        {
            if (i < 0)
            {
                return false;
            }
            else
            {
                return i % 2 == 0;
            }
        }


    }
}
