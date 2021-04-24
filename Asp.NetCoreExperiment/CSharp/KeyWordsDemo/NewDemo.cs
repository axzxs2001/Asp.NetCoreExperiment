using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace KeyWordsDemo
{
    class NewDemo : IDemo
    {
        public void Run()
        {
            //new 1
            var table = new DataTable();
            
            //new 2
            Show<Parent>(new Child());

            //new 3
            Parent parent = new Child();
            parent.Print();
        }
        //new 2
        static void Show<T>(T t) where T : new()
        {
            WriteLine(t.GetType());
        }

        #region new 3
        public class Parent
        {
            public virtual void Print()
            {
                WriteLine("Parent Print");
            }
            public virtual void View()
            {
            }
        }
        public class Child : Parent
        {
            //now 3
            public new void Print()
            {
                WriteLine("Child Print");
            }
            public override void View()
            {
            }
        }
        #endregion


    }
}
