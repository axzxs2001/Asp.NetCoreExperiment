using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace VersionDemo.Controllers
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
        public string Get()
        {
            var verStr = new StringBuilder(@"
.csproj文件
<Project Sdk=""Microsoft.NET.Sdk.Web"">
  <PropertyGroup>
    <TargetFramework>netcoreapp3.1</TargetFramework>
    <Version>1.1.1.1-beta</Version>
    <AssemblyVersion>2.2.2.2</AssemblyVersion>
    <FileVersion>3.3.3.3</FileVersion>
    <InformationalVersion>v1.2.3.4</InformationalVersion>
  </PropertyGroup>
</Project>
");
            verStr.AppendLine();
            verStr.AppendLine($"程序集版本号AssemblyVersion: " +

                              $"{Assembly.GetEntryAssembly().GetName().Version}");

            verStr.AppendLine($"文件版本号FileVersion:" +

                              $"{Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version}");

            verStr.AppendLine($"包版本Version号或产品InformationalVersion版本号:" +

                              $"{Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion}");

            return verStr.ToString();
        }
    }


}
