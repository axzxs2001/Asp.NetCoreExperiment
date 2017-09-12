using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace TokenWebApi.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            var disco =  DiscoveryClient.GetAsync("https://demo.identityserver.io").Result;
            if (disco.IsError) throw new Exception(disco.Error);
            var tokenClient = new TokenClient(
                disco.TokenEndpoint,
                "client",
                "secret");
            var tokenResponse =  tokenClient.RequestClientCredentialsAsync().Result;

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            Console.WriteLine( apiClient.GetStringAsync("http://localhost:56181/api/values").Result);


        }
    }
}
