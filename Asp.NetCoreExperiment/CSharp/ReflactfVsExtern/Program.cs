using System.Runtime.CompilerServices;
var test = new TestClass();

Console.WriteLine(Method(test));



[UnsafeAccessor(UnsafeAccessorKind.Method, Name = "Method")]
static extern DateTime Method(TestClass test);




public class TestClass
{

    DateTime Method()
    {
        return DateTime.Now;
    }
}