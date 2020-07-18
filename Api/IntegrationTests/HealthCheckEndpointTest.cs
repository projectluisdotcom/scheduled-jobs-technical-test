using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTests
{
    public class HealthCheckEndpointTest : IClassFixture<WebApplicationFactory<Api.Startup>>
    {
        private readonly WebApplicationFactory<Api.Startup> _factory;

        public HealthCheckEndpointTest(WebApplicationFactory<Api.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetHealthCheckOk()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/healthcheck");
            response.EnsureSuccessStatusCode();
        }
    }
}