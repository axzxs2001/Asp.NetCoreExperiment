namespace ABC
{
    public class MyClass
    {
        public void Print(string s)
        {
            Write(s);
        }
        void Write(string s)
        {
            System.Console.WriteLine($"MyClass的Print方法，参数是：{s}");
        }

    }
}
