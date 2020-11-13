using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLuaSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {


         
        
                Console.WriteLine("函数Demo");
                var tel = "123-4567-89ab";
                var functionBody = @"
function GetTel(tel)
    local newStr=string.gsub(tel,""-"","""")                
    return string.sub(newStr,1,4)..""-""..string.sub(newStr,5,8)..""-"".. string.sub(newStr,9,-1)
end
";
                foreach (var obj in ExecFunction("GetTel", functionBody, tel))
                {
                    Console.WriteLine(obj);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            Console.WriteLine("------------------");
            Console.WriteLine("表达式Demo");
            Console.WriteLine(ExecExpression<double>("y=10+x*(5 + 2)", "y", new KeyValuePair<string, double>("x", 10.25)));

            Console.WriteLine("------------------");
            Console.WriteLine("反调C#实例Demo");
            InvockClassObject();
        }

        /// <summary>
        /// lua中反调c#
        /// </summary>
        static void InvockClassObject()
        {
            var lua = new Lua();
            var obj = new MyClass();
            lua["obj"] = obj;
            lua.LoadCLRPackage();
            lua.DoString(@"local res1 = obj:Func1()
print(res1)
");
            lua.DoString(@"local res2 = obj:AnotherFunc(111,""aaa"")
print(res2)
");
            lua.DoString(@" import ('NLuaSample', 'NLuaSample')  ");
            lua.DoString(@"local res3 = MyClass.StaticMethod(123)
print(res3)
");

        }

        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="functionName">函数名</param>
        /// <param name="function">函数体</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        static object[] ExecFunction(string functionName, string function, params object[] parameters)
        {
            var lua = new Lua();
            lua.State.Encoding = Encoding.UTF8;
            lua.DoString(function);
            var scriptFunc = lua[functionName] as LuaFunction;
            return scriptFunc.Call(parameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="expression">表达式：y=10+x*(5 + 2)</param>
        /// <param name="returnValue">返回值名称</param>
        /// <param name="parameters">参数名称和值对</param>
        /// <returns></returns>
        static T ExecExpression<T>(string expression, string returnValue, params KeyValuePair<string, T>[] parameters)
        {
            var lua = new Lua();
            foreach (var parameter in parameters)
            {
                lua[parameter.Key] = parameter.Value;
            }
            lua.DoString(expression);
            return (T)lua[returnValue];
        }
    }

    public class MyClass
    {
        public int Func1()
        {
            return 32;
        }

        public string AnotherFunc(int val1, string val2)
        {
            return "Some String" + val1 + val2;
        }

        public static string StaticMethod(int param)
        {
            return "Return of Static Method" + param;
        }
    }
}
