using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IntegrationTests
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> GetJsonBody<T>(this HttpResponseMessage response)
            where T : class
        {
            var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }
    }
}