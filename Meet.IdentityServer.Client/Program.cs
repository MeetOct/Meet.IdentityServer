using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Meet.IdentityServer.Client
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            //
            var dico = await DiscoveryClient.GetAsync("http://localhost:59381");

            //token
            var tokenClient = new TokenClient(dico.TokenEndpoint, "client", "secret");
            var tokenresp = await tokenClient.RequestClientCredentialsAsync("api.policy");
            if (tokenresp.IsError)
            {
                Console.WriteLine(tokenresp.Error);
                Console.ReadKey();
            }

            Console.WriteLine(tokenresp.Json);
            Console.WriteLine("\n\n");


            var client = new HttpClient();
            client.SetBearerToken(tokenresp.AccessToken);

            var resp = await client.GetAsync("http://localhost:60710/identity");
            if (!resp.IsSuccessStatusCode)
            {
                Console.WriteLine(resp.StatusCode);
            }
            else
            {
                var content = await resp.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            var resp1 = await client.GetAsync("http://localhost:60710/identity/1");
            if (!resp1.IsSuccessStatusCode)
            {
                Console.WriteLine(resp1.StatusCode);
            }
            else
            {
                var content = await resp1.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
            Console.ReadKey();
        }
    }
}
