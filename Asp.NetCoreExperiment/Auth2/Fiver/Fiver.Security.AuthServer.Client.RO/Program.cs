using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace Fiver.Security.AuthServer.Client.RO
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // discover endpoints from metadata
                var disco = DiscoveryClient.GetAsync("http://localhost:5000").Result;

                // request token
                var tokenClient = new TokenClient(disco.TokenEndpoint, "fiver_auth_client_ro", "secret");
                var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("james", "password", "fiver_auth_api").Result;

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    return;
                }

                //Console.WriteLine(tokenResponse.Json);

                // call api
                var client = new HttpClient();
                client.SetBearerToken(tokenResponse.AccessToken);

                var response = client.GetAsync("http://localhost:5001/movies").Result;
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(JArray.Parse(content));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}