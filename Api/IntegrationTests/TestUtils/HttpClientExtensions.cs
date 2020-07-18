using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IntegrationTests
{
    public static class TestsUtils
    {
        public static async Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient client, string url, T request)
            where T : class
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            var body = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            return await client.PostAsync(url, body);
        }
    }
}