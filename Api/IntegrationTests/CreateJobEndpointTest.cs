using System.Threading.Tasks;
using Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTests
{
    public class CreateJobEndpointTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public CreateJobEndpointTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanCreateJobs()
        {
            var client = _factory.CreateClient();
            var request = new CreateJobsRequest
            {
                Data = new[]
                {
                    "random data 1",
                    "random data 2"
                },
                Type = "Bulk"
            };

            var response = await client.PostJsonAsync("/api/v1/job", request);
            response.EnsureSuccessStatusCode();

            var bodyResponse = await response.GetJsonBody<CreateJobResponse>();

            Assert.Equal(2, bodyResponse.DataPointsCreated);
        }
    }
}