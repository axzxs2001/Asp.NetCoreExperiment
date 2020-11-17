using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLuaAndRoslynCompare5
{
    public class NLuaClass
    {
        public static object[] ExecFunction(string functionName, string function, params object[] parameters)
        {
            var lua = new Lua();
            lua.State.Encoding = Encoding.UTF8;
            lua.DoString(function);
            var scriptFunc = lua[functionName] as LuaFunction;
            return scriptFunc.Call(parameters);
        }
    }
}
