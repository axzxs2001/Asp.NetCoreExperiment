using System;
using System.Collections.Generic;
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

        [UnmanagedCallersOnly(EntryPoint = "get_strings")]
        public static IntPtr StringTest()
        {
            var arr = new string[] { "a", "b", "c" }; 
          
            return Marshal.UnsafeAddrOfPinnedArrayElement(arr, 0);         
        }
    }
}

