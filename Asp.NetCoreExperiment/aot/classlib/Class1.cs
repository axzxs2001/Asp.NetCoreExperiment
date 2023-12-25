using System.Runtime.InteropServices;

namespace classlib
{
    public class class1
    {
        [NativeCallable(EntryPoint = "add_dotnet", CallingConvention = CallingConvention.Cdecl)]
        public static int Add(int a, int b)
        {
            return a + b;
        }
    }
}

namespace System.Runtime.InteropServices
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class NativeCallableAttribute : Attribute
    {
        public string EntryPoint;
        public CallingConvention CallingConvention;
        public NativeCallableAttribute() { }
    }
}