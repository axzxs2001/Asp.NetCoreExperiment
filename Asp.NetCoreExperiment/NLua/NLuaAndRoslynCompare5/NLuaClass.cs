using NLua;
using System.Text;

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