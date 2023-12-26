using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace classlib
{
    //发布命令 dotnet publish /p:NativeLib=Shared --use-current-runtime
    public static class DllTest
    {
        [UnmanagedCallersOnly(EntryPoint = "int_add")]
        public static int Add(int a, int b)
        {
            return a + b;
        }

        [UnmanagedCallersOnly(EntryPoint = "string_add")]
        public static IntPtr StringTest(IntPtr ptr1, IntPtr ptr22)
        {
            var s = Marshal.PtrToStringUTF8(ptr1) + Marshal.PtrToStringUTF8(ptr22);
            return Marshal.StringToCoTaskMemUTF8(s);
        }

        [UnmanagedCallersOnly(EntryPoint = "get_doubles")]
        public static IntPtr StringTest()
        {
            return Marshal.UnsafeAddrOfPinnedArrayElement(new double[] { 1.1, 1.2 }, 0);
        }

        [UnmanagedCallersOnly(EntryPoint = "set_doubles", CallConvs = new[] { typeof(CallConvCdecl) })]
        public static double SetAll(IntPtr InItems, int InItemsLength)
        {
            var sum = 0d;
            for (int i = 0; i < InItemsLength; i++)
            {
                sum += Marshal.PtrToStructure<double>(InItems + i * Marshal.SizeOf<double>());
            }
            return sum;
        }
    }
}

