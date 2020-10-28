using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ArchitectureDemo02
{
    public class Program
    {
        static void Main(string[] args)
        {
            //访问修饰符
            var myList = new MyList<int>();
            myList.Add(1);
            myList.Add(2);
            myList.Add(3);
            myList.Add(4);
            myList.Add(5);
            foreach (var o in myList)
            {
                Console.WriteLine(o);
            }
        }
    }

    public class MyList<T> : IEnumerable
    {
        protected T[] array;
        public MyList()
        {
            array = new T[4];
        }
        public int Count
        {
            get;
            private set;
        } = 0;

        public void Add(T t)
        {
            if (array.Length == Count)
            {
                array = CreateNewArray(array, Count * 2);
            }
            array[Count] = t;
            Count++;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return array[i];
            }
        }
        private T[] CreateNewArray(T[] oldArray, int length)
        {
            var newArray = new T[length];
            oldArray.CopyTo(newArray, 0);
            return newArray;
        }
    }

    public class OddEvenList : IEnumerable
    {
        protected readonly List<long> _odds;

        protected readonly List<long> _evens;

        protected readonly List<long> _list;

        private readonly Dictionary<int, Tuple<int, string>> _tupleDic;

        public OddEvenList()
        {
            _odds = new List<long>();
            _evens = new List<long>();
            _tupleDic = new Dictionary<int, Tuple<int, string>>();
        }

        public void Add(long no)
        {
            var noIndex = _list.Count;
            _list.Add(no);
            if (no % 2 == 0)
            {
                _tupleDic.Add(noIndex, new Tuple<int, string>(_evens.Count, "even"));
                _evens.Add(no);
            }
            else
            {
                _tupleDic.Add(noIndex, new Tuple<int, string>(_odds.Count, "odd"));
                _odds.Add(no);
            }
        }
        public void RemoveAt(int index)
        {
            switch (_tupleDic[index].Item2)
            {
                case "even":
                    _evens.RemoveAt(_tupleDic[index].Item1);
                    break;
                case "odd":
                    _odds.RemoveAt(_tupleDic[index].Item1);
                    break;
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var v in _odds)
            {
                yield return v;
            }
        }


        public int OddCount
        {
            get
            {
                return _odds.Count;
            }
        }
        public int EvenCount
        {
            get
            {
                return _evens.Count;
            }
        }
        public int Count
        {
            get
            {
                return _list.Count;
            }
        }


    }




}

