using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api;
using Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace IntegrationTests
{
    public class GetJobStateEndpointTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public GetJobStateEndpointTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanFetchJobStates()
        {
            var client = _factory.CreateClient();
            var request = new CreateJobsRequest
            {
                Data = new []
                {
                    "random data 1",
                    "random data 2"
                },
                Type = "Bulk"
            };
            
            var jsonRequest = JsonConvert.SerializeObject(request);
            await client.PostAsync("/api/v1/job", new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
            
            var createResponse = await client.GetAsync("/api/v1/job/state");
            var statesBody = await createResponse.GetJsonBody<JobsStateResponse>();
            Assert.NotEmpty(statesBody.States);
            Assert.NotEmpty(statesBody.States[0].Id);
        }
    }
}