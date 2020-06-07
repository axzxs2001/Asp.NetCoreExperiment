using System;

namespace CustomerFuncations
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = "${Replace(@content,1,3)}";

            var content = "1234babcd";



        }


    }

    class Functions
    {
        public string Replace(string content, string oldString, string newString)
        {
            return content.Replace(oldString, newString);
        }
    }
}
