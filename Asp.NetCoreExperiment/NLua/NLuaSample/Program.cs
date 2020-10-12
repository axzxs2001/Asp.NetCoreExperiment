using NLua;
using System;
using System.Linq;

namespace NLuaSample
{
	class Program
	{
		static void Main(string[] args)
		{
			F();
			F2();
		}
		static void F2()
        {
			var state = new Lua();
			double val = 12.0;
			state["x"] = val;
			state.DoString("y = 10 + x*(5 + 2)");
			double y = (double)state["y"];
			Console.WriteLine(y);
		}
		static void F()
		{
			var state = new Lua();
			state.DoString(@"
	function ScriptFunc (val1, val2)
		if val1 > val2 then
			return val1 + 1
		else
			return val2 - 1
		end
	end
	");
			var scriptFunc = state["ScriptFunc"] as LuaFunction;
			var res = scriptFunc.Call(3, 5).First();
			Console.WriteLine(res);
		}
	}
}
