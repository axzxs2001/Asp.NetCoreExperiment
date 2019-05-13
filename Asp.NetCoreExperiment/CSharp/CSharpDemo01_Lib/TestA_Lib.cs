using System;

namespace CSharpDemo01_Lib
{
    public class TestA_Lib
    {
        private protected void A()
        {
            Console.WriteLine("TestA.A");
        }
        public void B()
        {
            A();
        }
    }
}
