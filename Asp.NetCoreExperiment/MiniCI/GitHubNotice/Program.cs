using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GitHubNotice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
  

info: GitHubNotice.Controllers.HomeController[0]
      User - Agent = GitHub - Hookshot / 536e04f
info: GitHubNotice.Controllers.HomeController[0]
      Content - Length = 9909
info: GitHubNotice.Controllers.HomeController[0]
      X - GitHub - Delivery = cc712792 - ad77 - 11eb - 8939 - 82c91e1ecf77
              info: GitHubNotice.Controllers.HomeController[0]
      X - GitHub - Event = push
info: GitHubNotice.Controllers.HomeController[0]
      X - GitHub - Hook - ID = 295723396
info: GitHubNotice.Controllers.HomeController[0]
      X - GitHub - Hook - Installation - Target - ID = 351990081
info: GitHubNotice.Controllers.HomeController[0]
      X - GitHub - Hook - Installation - Target - Type = repository
info: GitHubNotice.Controllers.HomeController[0]
      X - Hub - Signature = sha1 = 1a58d4a0695ced6e95002ab26aa5f8c1f4cc336b
        info: GitHubNotice.Controllers.HomeController[0]
      X - Hub - Signature - 256 = sha256 = 74a759acc2efa26e2184c99e701aa2759e1d93769efef73
4a4a39b63eb2b9897




             */
            //var a = "74a759acc2efa26e2184c99e701aa2759e1d93769efef73";
            //var b = "1a58d4a0695ced6e95002ab26aa5f8c1f4cc336b";
            //var key = "e0f0d18348fbcb090fa17f9fbc638a8be56be3ab";

            //var s = IsGitHubSignatureValid(key, a, b);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });



      
    }
}
