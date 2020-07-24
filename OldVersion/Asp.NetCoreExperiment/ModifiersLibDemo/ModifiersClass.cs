using System;

namespace ModifiersLibDemo
{
    public class ModifiersClass
    {
        /// <summary>
        /// 能在本程序集内外继承子类或实例化后调用
        /// </summary>
        public void PublicMethod()
        {
            Console.WriteLine("PublicMethod");
        }
        /// <summary>
        /// 只能在继承子类中调用
        /// </summary>
        protected void ProtectedMethod()
        {
            Console.WriteLine("ProtectedMethod");
        }
        /// <summary>
        /// 只能本类调用
        /// </summary>
        private void PrivateMethod()
        {
            Console.WriteLine("PrivateMethod");
        }
        /// <summary>
        /// 只能在本程序集(dll,exe)继承子类或实例化后调用
        /// </summary>
        internal void InternalMethod()
        {
            Console.WriteLine("InternalMethod");
        }
        /// <summary>
        /// 只能在本程序集(dll,exe)内外继承子类中或本程序集(dll,exe)中实例化后调用
        /// </summary>
        protected internal void ProtectedInternalMethod()
        {
            Console.WriteLine("ProtectedInternalMethod");
        }
        /// <summary>
        /// 只能在本程序集(dll,exe)内的继承子类中调用
        /// </summary>
        private protected void PrivateProtectedMethod()
        {
            Console.WriteLine("PrivateProtectedMethod");
        }
    }


    public class TestModifiers : ModifiersLibDemo.ModifiersClass
    {
        public void Test()
        {
            PublicMethod();
            InternalMethod();
            ProtectedInternalMethod();
            ProtectedMethod();
            PrivateProtectedMethod();
        }

        public void Test2()
        {
            var test2 = new ModifiersClass();
            test2.InternalMethod();
            test2.ProtectedInternalMethod();
            test2.PublicMethod();
        }
    }
}
