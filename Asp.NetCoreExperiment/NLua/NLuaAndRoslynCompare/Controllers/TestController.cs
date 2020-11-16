using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLuaAndRoslynCompare.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {


        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get(int i)
        {
            if (i == 1)
            {
                return Roslyn();
            }
            else
            {
                return NLua();
            }

        }
        List<string> Roslyn()
        {
            var list = new List<string>();
            var csharp = @"return DateTime.Now.ToString(""yyyy-MM-dd HH:mm:ss.ffffff""); ";
            for (int i = 0; i < 1000; i++)
            {
                list.Add(RoslynClass.Transform(csharp, 1).ToString());
            }
            GC.Collect();
            return list;
        }

        List<string> NLua()
        {
            var functionBody = @"
function GetTel(tel)
    local newStr=string.gsub(tel,""-"","""")                
    return string.sub(newStr,1,4)..""-""..string.sub(newStr,5,8)..""-"".. string.sub(newStr,9,-1)
end
";
            var list = new List<string>();
            var tel = "123-4567-89ab";
            for (int i = 0; i < 1000; i++)
            {
                foreach (var obj in NLuaClass.ExecFunction("GetTel", functionBody, tel))
                {
                    list.Add(obj.ToString());
                }
            }
            return list;
        }
    }
}
