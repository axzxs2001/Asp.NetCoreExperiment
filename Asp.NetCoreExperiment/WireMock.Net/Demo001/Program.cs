using System;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace Demo001
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = WireMockServer.Start();
            foreach (var url in server.Urls)
            {
                Console.WriteLine(url);
            }
            var path = System.IO.Directory.GetCurrentDirectory();
            server.ReadStaticMappings(path);
            Console.WriteLine(path);
            //server
            //    .Given(Request.Create().WithPath("/exact")
            //        .WithParam("from", new ExactMatcher("abc")))
            //    .RespondWith(Response.Create()
            //        .WithBody("Exact match")
            //    );
            Console.ReadKey();
        }
    }
}
