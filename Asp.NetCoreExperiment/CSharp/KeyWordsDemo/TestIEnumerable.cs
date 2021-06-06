using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace KeyWordsDemo
{
    class TestIEnumerable : IDemo
    {
        public void Run()
        {
            var myEnumerable = new MyList();
            foreach (var e in myEnumerable)
            {
                WriteLine(e);
            }
           
            var item = new Itmes().GetItem();
            while (item != null && item.MoveNext())
            {
                WriteLine(item.Current);
            }           
        }        
    }
    class Itmes
    {
        string[] arr = new string[] { "a", "b", "c" };
        public IEnumerator GetItem()
        {
            for (var i = 0; i < arr.Length; i++)
            {
                yield return arr[i];
            }
        }
    }

    class MyList : IEnumerable
    {
        string[] arr = new string[] { "a", "b", "c" };
        public IEnumerator GetEnumerator()
        {
            for (var i = 0; i < arr.Length; i++)
            {
                yield return arr[i];
            }
        }
    }
}
